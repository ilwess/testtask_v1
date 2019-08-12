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
        // GET: Shop/Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(ShoppingCart<Product> cart, string name)
        {
            ProductContext pc = new ProductContext();
            Product product = pc.Prods.First(o => o.Name == name);
            cart.Add(product);
            return RedirectToAction("List", "Products");
        }

        public ViewResult ShowCart(ShoppingCart<Product> cart)
        {
            return View(cart);
        }
    }
}