using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    [Serializable]
    public class Product
    {

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public virtual List<Order> Orders
        {
            get;
            set;
        }

        public Product(string name, double price, string descr)
        {
            this.Name = name;
            this.Price = price;
            this.Description = descr;
        }

        public Product()
        {

        }
    }
}