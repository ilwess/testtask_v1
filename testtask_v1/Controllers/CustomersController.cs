using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;

namespace testtask_v1.Controllers
{
    public class CustomersController : Controller
    {
        ProductContext pc = new ProductContext();
        public string Index()
        {
            return "Customers.Index";
        }
        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(Product prod)
        {
            pc.Prods.Add(prod);
            pc.SaveChanges();
            return "Product was added";
        }


    }
}