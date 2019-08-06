using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class CustomerOrder
    {
        public int Id
        {
            get;
            set;
        }

        public Customer Orderer { get; set; }
        
        public List<Product> Products
        {
            get;
            set;
        }
        
    }
}