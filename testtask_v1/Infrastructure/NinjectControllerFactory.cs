using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using Domain.Abstract;
using BLL.Concrete;
using Domain.Concrete;
using Domain.EXContexts;
using BLL.Abstract;

namespace testtask_v1.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(
            RequestContext context,
            Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>();
            //ninjectKernel.Bind<IRepository<Product>>()
            //    .To<Repository<Product>>();

            //ninjectKernel.Bind<IRepository<Order>>()
            //    .To<Repository<Order>>();

            //ninjectKernel.Bind<IRepository<Customer>>()
            //    .To<Repository<Customer>>();
            ninjectKernel.Bind<ShopContext>()
                .ToSelf();

            ninjectKernel.Bind<IOrderService>()
                .To<OrderService>();
            ninjectKernel.Bind<IProductService>()
                .To<ProductService>();
        }
    }
}