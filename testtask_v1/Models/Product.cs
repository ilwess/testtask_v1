using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class Product
    {
        private static int idIncrementer = 0;
        
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

        public Product(string name, double price, string descr)
        {
            this.Id = idIncrementer;
            idIncrementer++;
            this.Name = name;
            this.Price = price;
            this.Description = descr;
        }

        public Product()
        {
            this.Id = idIncrementer;
            idIncrementer++;
        }
    }
}