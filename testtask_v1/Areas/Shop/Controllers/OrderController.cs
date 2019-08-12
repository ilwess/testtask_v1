using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testtask_v1.Models;

namespace testtask_v1.Areas.Shop.Controllers
{
    public class OrderController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MakeOrder()
        {
            Customer customer = db.Customers
                    .Single(o => o.Email == User.Identity.Name);

           
            CustomerOrder newOrder = new CustomerOrder()
            {
                Orderer = customer,
                Products = ((ShoppingCart<Product>)Session["Cart"]).products,
                Date = DateTime.Now,
            };
            //newOrder.Products = new List<Product>(((ShoppingCart<Product>)Session["Cart"]).products.Count());
            //Array.Copy(((ShoppingCart<Product>)Session["Cart"]).products, newOrder.Products, ((ShoppingCart<Product>)Session["Cart"]).products.Count());

            db.Orders.Add(newOrder);
            db.SaveChanges();
            var ss = db.Orders.ToList();
            return View(ss);
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View(db.Orders.
                Include(o => o.Products).
                Include(o => o.Orderer).
                ToList());
        }

        [HttpPost]
        public ActionResult Orders(DateTime StartDate, DateTime EndDate)
        {
            IEnumerable<CustomerOrder> orders;
            orders = db.Orders.Include(o => o.Products).Include(o=>o.Orderer).ToList<CustomerOrder>().Where(o => ((o.Date >= StartDate) && (o.Date <= EndDate)));
            return View(orders);
        }

        [HttpGet]
        public ActionResult OrdersByCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrdersByCustomer(string customerName)
        {
            IEnumerable<CustomerOrder> customerOrders = 
                db.Orders
                .Include(o => o.Orderer)
                .Include(o => o.Products)
                .Where(o => o.Orderer.Email == customerName);
            return View(customerOrders);
        }
    }
}