using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;

namespace testtask_v1.Areas.Shop.Controllers
{
    public class CartController : Controller
    {
        private IProductService productService;

        public ActionResult Index()
        {
            return View();
        }

        public CartController(IProductService prodService)
        {
            productService = prodService;
        }

        public ActionResult AddToCart(ShoppingCart<ProductDTO> cart, string name)
        {
            ProductDTO product = productService
                .Get(p => p.Name == name)
                .FirstOrDefault();
            cart.Add(product);
            return RedirectToAction("List", "Products");
        }

        public ViewResult ShowCart(ShoppingCart<ProductDTO> cart)
        {
            return View(cart);
        }
    }
}