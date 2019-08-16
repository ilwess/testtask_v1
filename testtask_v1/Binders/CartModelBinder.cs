using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Domain.Entities;
using BLL.DTO;

namespace testtask_v1.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            ShoppingCart<ProductDTO> cart = (ShoppingCart<ProductDTO>)controllerContext.HttpContext.Session[sessionKey];
            if(cart is null)
            {
                cart = new ShoppingCart<ProductDTO>();
                controllerContext.HttpContext.Session["Cart"] = cart;
            }
            return cart;
        }
    }
}