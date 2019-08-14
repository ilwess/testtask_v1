using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;
using Domain.Abstract;
using Domain.Entities;

namespace testtask_v1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Admin/Admin
        public ActionResult List()
        {
            return View(unitOfWork.Products.Get().ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Product prod)
        {
            unitOfWork.Products.Add(prod);
            await unitOfWork.CommitAsync();
            return RedirectToAction("List", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int prodId)
        {
            Product prod = unitOfWork.Products.Get(p => p.Id == prodId).FirstOrDefault();
            
            if (prod != null)
            {
                unitOfWork.Products.Remove(prod);
                await unitOfWork.CommitAsync();
            }
            return RedirectToAction("List", "Admin");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult EditProduct(int prodId)
        {
            Product prodToEdit = unitOfWork
                .Products
                .Get(p => p.Id == prodId).FirstOrDefault();
            return View(prodToEdit);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(
            int prodId, string newName,
            double newPrice, string newDescription)
        {
            Product prodToEdit = 
                await unitOfWork.Products.FindAsync(prodId);
            prodToEdit.Name = newName;
            prodToEdit.Price = newPrice;
            prodToEdit.Description = newDescription;
            unitOfWork.Products.Update(prodToEdit);
            await unitOfWork.CommitAsync();
            return RedirectToAction("List", "Admin");
        }
    }
}