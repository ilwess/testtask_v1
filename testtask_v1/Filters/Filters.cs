using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testtask_v1.Filters
{
    public class HttpRequestExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(!filterContext.ExceptionHandled && filterContext.Exception is HttpException)
            {
                string logMsg = "";
                logMsg += filterContext.Exception.Message;
                filterContext.ExceptionHandled = true;

            }
        }
    }
}