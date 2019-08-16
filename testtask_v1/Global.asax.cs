using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using testtask_v1.Models;
using testtask_v1.Binders;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Domain.Entities;
using BLL.DTO;

namespace testtask_v1
{


    public class MvcApplication : System.Web.HttpApplication
    {
        public static int queryCount = 0;
        protected string email = "pasha.vrublevskiy20@list.ru";
        protected void Application_Start()
        {
            App_Start.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer(new ShopDbInitializer());
            Logger.Logger.InitLogger();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(
                typeof(ShoppingCart<ProductDTO>),
                new CartModelBinder());
            ControllerBuilder.Current.SetControllerFactory(
                new Infrastructure.NinjectControllerFactory());
        }

        protected void Application_BeginRequest()
        {
            queryCount++;
        }
    }
}
