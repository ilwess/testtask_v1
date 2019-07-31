using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using testtask_v1.Models;

namespace testtask_v1.Controllers
{
    public class ProductsController : Controller
    {
        ProductContext pc = new ProductContext();
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
            IEnumerable<Product> products = pc.Prods;

            ViewBag.prods = products;
            
            return View();
        }
    }
}