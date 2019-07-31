using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class Product
    {
        private int id;
        private string name;
        private double price;
        private string description;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        public Product(int id, string name, double price, string descr)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = descr;
        }
    }
}