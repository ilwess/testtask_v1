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

        public ActionResult AddToCart(ShoppingCart<ProductDTO> cart, string name)
        {
            ProductDTO product = productService
                .Products
                .Get()
                .First(o => o.Name == name);
            cart.Add(product);
            return RedirectToAction("List", "Products");
        }

        public ViewResult ShowCart(ShoppingCart<ProductDTO> cart)
        {
            return View(cart);
        }
    }
}