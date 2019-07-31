using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public string Index()
        {
            return "Products.Index";
        }

        public string Details()
        {
            return "Products.Details";
        }

        public ViewResult List()
        {
            List<Models.Product> products = new List<Models.Product>()
            {
                new Models.Product(0, "Pen", 1, "good pen)"),
                new Models.Product(1, "Ball", 5, "the best ball ))"),
                new Models.Product(2, "Plastic left leg", 10, "pretty good plastic leg for people without left leg"),
                new Models.Product(3, "Plastic right leg", 10, "pretty good plastic right leg for people without right leg"),
                new Models.Product(4, "Right and left plastic legs", 19, "nice set of legs with discount"),
            };

            ViewBag.prods = products;
            
            return View();
        }
    }
}