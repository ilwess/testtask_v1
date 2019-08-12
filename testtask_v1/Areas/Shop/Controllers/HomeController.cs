using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Areas.Shop.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ViewResult Index()
        {
            return View();
        }

        public string QueryCount()
        {
            return MvcApplication.queryCount.ToString();
        }
    }
}