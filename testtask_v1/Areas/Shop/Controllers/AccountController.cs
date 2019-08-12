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

        private AppRoleManager roleManager
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

        [HttpGet]
        public async Task<ViewResult> Register()
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
                await Manager.AddToRoleAsync(user.Id, "User");
                if (result.Succeeded)
                {
                    MailAddress from = new MailAddress(
                        "pasha.vrublevskiy20@list.ru",
                        "Email Confirmation");

                    MailAddress to = new MailAddress(user.Email);

                    MailMessage msg = new MailMessage(from, to);

                    msg.Subject = "Confirmation";

                    msg.Body = string.Format("To complete click: " +
                        "<a href=\"{0}\" title=\"Confirm registration\">{0}</a>",
                        Url.Action("ConfirmEmail", "Account",
                        new { Token = user.Id, Email = user.Email },
                        Request.Url.Scheme
                        ));
                    msg.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(
                        "pasha.vrublevskiy20@list.ru",
                        "ExortinvokeR122");
                    client.EnableSsl = true;
                    client.Timeout = 10000;

                    client.Send(msg);

                    using (ProductContext db = new ProductContext())
                    {
                        Customer customer = new Customer(user.Email, user.PhoneNumber);
                        db.Customers.Add(customer);
                        await db.SaveChangesAsync();
                    }

                    return RedirectToAction("Confirm", "Account", new { user.Email});
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
            User user = await Manager.FindByIdAsync(token);
            if(user != null)
            {
                if(user.Email == email)
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
                    return RedirectToAction("Confirm", "Account", new { email = user.Email});
                }
            }
            else
            {
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
                        return RedirectToAction("Confirm", "Account");
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
            InfoUser info = new InfoUser();
            info.Email = await Manager.GetEmailAsync(User.Identity.GetUserId());
            info.Phone = await Manager.GetPhoneNumberAsync(User.Identity.GetUserId());
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