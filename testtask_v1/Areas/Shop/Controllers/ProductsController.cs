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

namespace testtask_v1.Areas.Shop.Controllers
{
    public class ProductsController : Controller
    {
        IUnitOfWork unitOfWork;

        public ProductsController(IUnitOfWork iow)
        {
            unitOfWork = iow;
        }
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
            var groupedProds = from p in unitOfWork.Products.Get()
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
            Product productToExport = unitOfWork
                .Products
                .Get(p => p.Name == nameOfProduct)
                .First();
            XmlSerializer formatter = new XmlSerializer(typeof(Product));
            using(FileStream fs = new FileStream(@"C:\Users\Админ\Documents\Visual Studio 2017\Projects\testtask_v1\testtask_v1\Products\Product\" + "Product" + productToExport.Id + ".xml", FileMode.Create))
            {
                formatter.Serialize(fs, productToExport);
            }
            return RedirectToAction("List", "Products");
        }

        public ActionResult ExportAllProducts()
        {
           
            XmlSerializer formatter = new XmlSerializer(typeof(List<Product>));
            using(FileStream fs
                = new FileStream(
                    @"C:\Users\Админ\Documents\Visual Studio 2017\Projects\testtask_v1\testtask_v1\Products\Products.xml",
                    FileMode.Create))
            {
                formatter.Serialize(
                    fs,
                    unitOfWork.Products.Get().ToList());
            }
            return RedirectToAction("List", "Products");
        }


    }
}