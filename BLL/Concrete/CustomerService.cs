using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork db;

        public CustomerService(IUnitOfWork iow)
        {
            db = iow;
        }

        public async Task AddAsync(CustomerDTO newCustomer)
        {
            Customer newCust = new Customer()
            {
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,
            };
            db.Customers.Add(newCust);
            await db.CommitAsync();
        }
    }
}
