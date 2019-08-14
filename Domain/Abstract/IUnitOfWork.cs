using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<Customer> Customers { get; }

        void Commit();
        Task CommitAsync();

        void Reset();
        Task ResetAsync();
    }
}
