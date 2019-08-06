using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public string Index()
        {
            return "Exc.Index";
        }

        public ViewResult ExceptionFound()
        {
            return View();
        }
    }
}