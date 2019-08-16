using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Domain.Abstract;
using Domain.Entities;
using BLL.Abstract;
using BLL.DTO;

namespace testtask_v1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private IProductService productService;

        public AdminController(IProductService prodService)
        {
            productService = prodService;
        }
        // GET: Admin/Admin
        public ActionResult List()
        {
            return View(productService.Get());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProductDTO prod)
        {
            await productService.AddAsync(prod);
            return RedirectToAction("List", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int prodId)
        {
            await productService.DeleteAsync(prodId);
            return RedirectToAction("List", "Admin");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult EditProduct(int prodId)
        {
            ProductDTO prodToEdit = productService
                .Get(p => p.Id == prodId).FirstOrDefault();
            return View(prodToEdit);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(
            int prodId, string newName,
            double newPrice, string newDescription)
        {
            await productService.EditAsync(prodId, newName,
                newPrice, newDescription);
            return RedirectToAction("List", "Admin");
        }
    }
}