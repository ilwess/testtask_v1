using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using testtask_v1.ViewModels;

namespace testtask_v1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private AppRoleManager roleManager
        {
            get { return HttpContext
                    .GetOwinContext()
                    .GetUserManager<AppRoleManager>(); }
        }

        private async void AddRoles()
        {
            var adminRole = await roleManager.FindByIdAsync("Admin");
            var managerRole = await roleManager.FindByIdAsync("Manager");
            var userRole = await roleManager.FindByIdAsync("User");

            if(adminRole == null)
            {
                adminRole = new AppRole() { Name = "Admin", };
                managerRole = new AppRole() { Name = "Manager", };
                userRole = new AppRole() { Name = "User", };
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(managerRole);
                await roleManager.CreateAsync(userRole);
            }
        }

        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string roleName)
        {
            if (ModelState.IsValid)
            {
                AppRole newRole = new AppRole()
                {
                    Name = roleName,
                };

                await roleManager.CreateAsync(newRole);
                return RedirectToAction("Index", "Role");
            } else
            {
                ModelState.AddModelError("", "Oops.. Error");
            }
            return View("Index", "Role");
        }
    }
}