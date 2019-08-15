using BLL.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IOrderService
    {
        Task MakeOrder(OrderDTO newOrder);
        IEnumerable<OrderDTO> GetAll();
        IEnumerable<OrderDTO> Get(Func<Order, bool> predicate);

    }
}
