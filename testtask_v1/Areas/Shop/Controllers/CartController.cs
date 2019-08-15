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
        private IUnitOfWork unitOfWork;
        // GET: Shop/Cart
        public ActionResult Index()
        {
            return View();
        }

        public CartController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public void AddToCart(ShoppingCart<Product> cart, string name)
        {
            Product product = unitOfWork.Products.Get().First(o => o.Name == name);
            cart.Add(product);
            //return RedirectToAction("List", "Products");
        }

        public ViewResult ShowCart(ShoppingCart<Product> cart)
        {
            return View(cart);
        }
    }
}