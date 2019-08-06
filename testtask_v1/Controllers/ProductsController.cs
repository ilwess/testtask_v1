using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using testtask_v1.Models;
using testtask_v1.ViewModels;

namespace testtask_v1.Controllers
{
    public class ProductsController : Controller
    {
        ProductContext pc = new ProductContext();
        ShoppingCart<Product> cart = new ShoppingCart<Product>();
        // GET: Products
        public string Index()
        {
            return "Products.Index";
        }

        public string Details()
        {
            return "Products.Details";
        }

        public ViewResult List()
        {
            var groupedProds = from p in pc.Prods
                        group p by new { p.Name, p.Price, p.Description};
            List<ProductViewModel> prods = new List<ProductViewModel>();
            foreach(var prod in groupedProds)
            {
                prods.Add(new ProductViewModel(prod.Key.Name, 
                    prod.Key.Price, 
                    prod.Key.Description, 
                    prod.Count()
                    ));
            }
            return View(prods);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(Product prod)
        {
            pc.Prods.Add(prod);
            pc.SaveChanges();
            return "Product was added";
        }

        [HttpPost]
        public ActionResult AddToCart(string name)
        {
            Logger.Logger.Log.Debug(name);
            Product product = pc.Prods.First(o => o.Name == name);
            cart.Add(product);
            Logger.Logger.Log.Debug(cart.Count());
            return RedirectToAction("List");
        }

        public ActionResult ShowCart()
        {
            ViewBag.Cart = cart;
            return View();
        }


    }
}