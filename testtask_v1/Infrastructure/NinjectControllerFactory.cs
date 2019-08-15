using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Domain.EXContexts;

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

            ninjectKernel.Bind<IRepository<Product>>()
                .To<Repository<Product>>();

            ninjectKernel.Bind<IRepository<Order>>()
                .To<Repository<Order>>();

            ninjectKernel.Bind<IRepository<Customer>>()
                .To<Repository<Customer>>();

            ninjectKernel.Bind<ShopContext>()
                .ToSelf();
        }
    }
}