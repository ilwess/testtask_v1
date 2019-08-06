using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtask_v1.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public ProductViewModel(string name, double price, string descr, int count)
        {
            Name = name;
            Price = price;
            Description = descr;
            Count = count;
        }
    }
}