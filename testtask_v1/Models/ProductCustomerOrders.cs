using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class ProductCustomerOrders
    {
        public int Id
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }

        public CustomerOrder Order
        {
            get;
            set;
        }
    }
}