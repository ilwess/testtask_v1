using BLL.Abstract;
using BLL.DTO;
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
        IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MakeOrder(ShoppingCart<ProductDTO> cart)
        {
            CustomerDTO customer = new CustomerDTO()
            {
                Email = User.Identity.Name,
            };
            OrderDTO newOrder = new OrderDTO()
            {
                Date = DateTime.Now,
                Orderer = customer,
                Products = cart.products,
            };

            await orderService.MakeOrder(newOrder);
            return View(newOrder);
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View(orderService
                .GetAll()
                .ToList());
        }

        [HttpPost]
        public ActionResult Orders(DateTime StartDate, DateTime EndDate)
        {
            IEnumerable<OrderDTO> orders;
            orders = orderService
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
            IEnumerable<OrderDTO> customerOrders = 
                orderService
                .Get(o => o.Orderer.Email == customerName);
            return View(customerOrders);
        }
    }
}