using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;

namespace testtask_v1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Admin/Admin
        public ActionResult List()
        {
            return View(db.Prods.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Product prod)
        {
            db.Prods.Add(prod);
            await db.SaveChangesAsync();
            return RedirectToAction("List", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int prodId)
        {
            Product prod = db.Prods.Where(p => p.Id == prodId).FirstOrDefault();
            db.Entry(prod)
                .Collection(c => c.Orders)
                .Load();
            if (prod != null)
            {
                db.Prods.Remove(prod);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("List", "Admin");
        }
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult EditProduct(int prodId)
        {
            Product prodToEdit =  db.Prods.Where(p => p.Id == prodId).FirstOrDefault();
            return View(prodToEdit);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(
            int prodId, string newName,
            double newPrice, string newDescription)
        {
            Product prodToEdit = await db.Prods.FindAsync(prodId);
            prodToEdit.Name = newName;
            prodToEdit.Price = newPrice;
            prodToEdit.Description = newDescription;
            await db.SaveChangesAsync();
            return RedirectToAction("List", "Admin");
        }
    }
}