using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Concrete
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        
        public OrderService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<OrderDTO> Get(Func<Order, bool> predicate)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Order, OrderDTO>())
                .CreateMapper();
            return mapper
                .Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(db.Orders.Get(predicate));
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Order, OrderDTO>())
                .CreateMapper();
            return mapper
                .Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(db.Orders.Get());
        }

        public async Task MakeOrder(OrderDTO newOrder)
        {
            Customer orderer = await db.Customers
                .FindAsync(newOrder.Orderer.Id);
            List<Product> products = new List<Product>();
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDTO>())
                .CreateMapper();
            foreach(var prod in newOrder.Products)
            {
                products.Add(db.Products.Find(prod.Id));
            }
            Order order = new Order()
            {
                Date = newOrder.Date,
                Orderer = orderer,
                Products = products,
            };
            db.Orders.Add(order);
            await db.CommitAsync();
        }
    }
}
