using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Net;
using System.Net.Mail;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Entities;
using BLL.Abstract;
using BLL.DTO;

namespace testtask_v1.Areas.Shop.Controllers
{

    public class AccountController : Controller
    {
        private AppManager Manager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppManager>();
            }
        }
        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext
                  .GetOwinContext()
                  .GetUserManager<AppRoleManager>();
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private ICustomerService customerService;

        public AccountController(ICustomerService custService)
        {
            customerService = custService;
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUser rc)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = rc.Email,
                    Email = rc.Email,
                    EmailConfirmed = false,
                };

                IdentityResult result = await
                    Manager.CreateAsync(user, rc.Password);
                if(result.Succeeded)
                {
                    IdentityMessage message = new IdentityMessage();
                    message.Subject = "Email Confirmation";

                    message.Body = string.Format("To complete click: " +
                    "<a href=\"{0}\" title=\"Confirm registration\">link</a>",
                    Url.Action("ConfirmEmail", "Account",
                    new {
                        token = Manager.GenerateEmailConfirmationToken(user.Id),
                        email = user.Email },
                    Request.Url.Scheme
                    ));
                    message.Destination = user.Email;
                    await Manager.EmailService.SendAsync(message);
                    await Manager.AddToRoleAsync(user.Id, "User");
                }
                if (result.Succeeded)
                {
                    CustomerDTO customer = new CustomerDTO()
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    };
                    await customerService.AddAsync(customer);

                    return RedirectToAction(
                        "Confirm",
                        "Account",
                        new { email = user.Email});
                } else
                {
                    foreach (string error in result.Errors)
                        ModelState.AddModelError(" ", error);
                }
            }
            return View();
        }

        public ActionResult Confirm(string email)
        {
            return View(email);
        }

        public async Task<ActionResult> ConfirmEmail(
            string token,
            string email)
        {
            User user = await Manager.FindByEmailAsync(email);
            if(user != null)
            {
                IdentityResult emailConfirmRes =
                    await Manager.ConfirmEmailAsync(user.Id, token);
                if (emailConfirmRes.Succeeded)
                {
                    if (user.Email == email)
                    {
                        user.EmailConfirmed = true;
                        await Manager.UpdateAsync(user);
                        ClaimsIdentity claims = await Manager.CreateIdentityAsync(
                            user,
                            DefaultAuthenticationTypes.ApplicationCookie);
                        AuthManager.SignOut();
                        AuthManager.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = true,
                        }, claims);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Confirm", "Account", user.Email);
                    }
                }
                else
                {
                    var res = await Manager.DeleteAsync(user);
                    return RedirectToAction("Register", "Account");
                }
            }
            else
            {
                Logger.Logger.Log.Debug("null");
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login(string currUrl)
        {
            ViewBag.CurrUrl = currUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUser lc, string currUrl)
        {
            if (ModelState.IsValid)
            {
                User customer = await Manager.FindAsync(lc.Email, lc.Password);
                if(customer == null)
                {
                    ModelState.AddModelError(" ", "Wrong login or password!");
                } else {
                    if (customer.EmailConfirmed != false)
                    {
                        ClaimsIdentity claim = await Manager.CreateIdentityAsync(
                            customer,
                            DefaultAuthenticationTypes.ApplicationCookie);
                        AuthManager.SignOut();
                        AuthManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true,
                        }, claim);
                        if (!string.IsNullOrEmpty(currUrl))
                        {
                            return Redirect(currUrl);
                        }
                        else { return RedirectToAction("Index", "Home"); }
                    }
                    else
                    {
                        return View("Confirm", "Account", customer.Email);
                    }
                }
                ViewBag.CurrUrl = currUrl;
                return View(lc);
            }
            return View("~/Home/Index");
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return View("Login");
        }

        [Authorize]
        public async Task<ViewResult> Info()
        {
            InfoUser info = new InfoUser
            {
                Email = await Manager.GetEmailAsync(User.Identity.GetUserId()),
                Phone = await Manager.GetPhoneNumberAsync(User.Identity.GetUserId())
            };
            return View(info);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditNumber()
        {
            User customer = await Manager.FindByEmailAsync(User.Identity.Name);
            if (customer != null)
            {
                PhoneUser ic = new PhoneUser()
                {
                    CountryCode = "",
                    OperatorCode = "",
                    PhoneNumber = "",
                };
                return View(ic);
            }
            else return RedirectToAction("Info", "Account");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditNumber(PhoneUser ic)
        {
            User customer = await
                Manager.FindByEmailAsync(User.Identity.Name);
            if(customer != null)
            {
                customer.PhoneNumber = ic.FullPhoneNumber;
                IdentityResult res = await 
                    Manager.UpdateAsync(customer);
                if (res.Succeeded)
                {
                    return RedirectToAction("Info", "Account");
                }
                else ModelState.AddModelError(" ", "Somthing went wrong!...");
            } else {
                ModelState.AddModelError(" ", "User didnt found");
            }
            return View("Info");
        }
    }
}