using Domain.Abstract;
using Domain.Entities;
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
        IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MakeOrder(ShoppingCart<Product> cart)
        {
            Customer customer = unitOfWork
                .Customers
                .Get()
                .Single(o => o.Email == User.Identity.Name);

           
            Order newOrder = new Order()
            {
                Orderer = customer,
                Products = cart.products,
                Date = DateTime.Now,
            };

            unitOfWork.Orders.Add(newOrder);
            await unitOfWork.CommitAsync();
            return View(newOrder);
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View(unitOfWork.Orders
                .Get()
                .ToList());
        }

        [HttpPost]
        public ActionResult Orders(DateTime StartDate, DateTime EndDate)
        {
            IEnumerable<Order> orders;
            orders = unitOfWork.Orders
                .Get(o => ((o.Date >= StartDate) && (o.Date <= EndDate)))
                .ToList();
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
            IEnumerable<Order> customerOrders = 
                unitOfWork
                .Orders
                .Get(o => o.Orderer.Email == customerName);
            return View(customerOrders);
        }
    }
}