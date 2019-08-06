using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Filters
{
    public class AllExceptionHandleAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Logger.Logger.Log.Warn(filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }
}