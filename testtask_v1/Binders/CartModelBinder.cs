using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Domain.Entities;

namespace testtask_v1.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            ShoppingCart<Product> cart = (ShoppingCart<Product>)controllerContext.HttpContext.Session[sessionKey];
            if(cart is null)
            {
                cart = new ShoppingCart<Product>();
                controllerContext.HttpContext.Session["Cart"] = cart;
            }
            return cart;
        }
    }
}