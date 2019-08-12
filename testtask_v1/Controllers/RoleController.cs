using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace testtask_v1.Controllers
{
    public class RoleController : Controller
    {
        private AppRoleManager roleManager
        {
            get { return HttpContext
                    .GetOwinContext()
                    .GetUserManager<AppRoleManager>(); }
        }
        // GET: Role
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }
    }
}