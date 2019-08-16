using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using testtask_v1.Models;
using testtask_v1.Binders;
using BLL.DTO;
using AutoMapper;
using testtask_v1.App_Start;

namespace testtask_v1
{


    public class MvcApplication : System.Web.HttpApplication
    {
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
            MapperProfile.ConfigureMapper();
        }
    }
}
