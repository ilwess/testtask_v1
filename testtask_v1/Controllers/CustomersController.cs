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
        // GET: Customers
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
            
            return "Product was added";
        }


    }
}