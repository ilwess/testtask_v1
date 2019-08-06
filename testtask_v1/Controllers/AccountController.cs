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

namespace testtask_v1.Controllers
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

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
                };
                IdentityResult result = await
                    Manager.CreateAsync(user, rc.Password);
                if (result.Succeeded)
                {
                    using (ProductContext db = new ProductContext())
                    {
                        Customer customer = new Customer(user.Email, user.PhoneNumber);
                        db.Customers.Add(customer);
                    }
                    return RedirectToAction("Login", "Account");
                } else
                {
                    foreach (string error in result.Errors)
                        ModelState.AddModelError(" ", error);
                }
            }
            return View();
        }


        [HttpGet]
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
                    } else { return RedirectToAction("Index", "Home"); }
                }
                ViewBag.CurrUrl = currUrl;
                return View(lc);
            }
            return View("~/Home/Index");
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return View("Account", "Login");
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