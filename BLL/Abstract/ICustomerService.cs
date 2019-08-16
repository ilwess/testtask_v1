using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICustomerService
    {
        Task AddAsync(CustomerDTO newCustomer);
    }
}
