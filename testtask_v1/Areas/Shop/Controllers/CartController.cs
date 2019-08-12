using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Areas.Shop.Controllers
{
    public class CartController : Controller
    {
        // GET: Shop/Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}