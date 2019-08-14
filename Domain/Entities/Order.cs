using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class Order
    {
        public int Id
        {
            get;
            set;
        }

        public virtual Customer Orderer { get; set; }

        public virtual List<Product> Products
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

    }
}