using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using testtask_v1.Models;
using testtask_v1.ViewModels;
using System.Xml.Serialization;
using System.IO;
using Domain.Concrete;
using Domain.Abstract;
using Domain.Entities;
using BLL.Abstract;

namespace testtask_v1.Areas.Shop.Controllers
{
    public class ProductsController : Controller
    {
        IProductService productService;

        public ProductsController(IProductService prodService)
        {
            productService = prodService;
        }
        // GET: Products
        public string Index()
        {
            return "Products.Index";
        }

        public ViewResult List()
        {
            var groupedProds = from p in productService.GetAll()
                        group p by new { p.Name, p.Price, p.Description};
            List<ProductViewModel> prods = new List<ProductViewModel>();
            foreach(var prod in groupedProds)
            {
                prods.Add(new ProductViewModel(
                    prod.Key.Name, 
                    prod.Key.Price, 
                    prod.Key.Description, 
                    prod.Count()
                    ));
            }
            return View(prods);
        }

        

        public ActionResult Export(string nameOfProduct)
        {
            productService.Export(nameOfProduct);
            return RedirectToAction("List", "Products");
        }

        public ActionResult ExportAllProducts()
        {
            productService.ExportAll();
            return RedirectToAction("List", "Products");
        }


    }
}