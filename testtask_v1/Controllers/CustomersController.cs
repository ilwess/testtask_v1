using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public string Index()
        {
            return "Customers.Index";
        }

        public ViewResult Add()
        {
            return View();
        }
    }
}