using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testtask_v1.ViewModels;


namespace testtask_v1.Models
{
    public class ShoppingCart<TProduct> : IEnumerable<TProduct>
        where TProduct : Product
    {
        public List<TProduct> products = new List<TProduct>();
        public IEnumerator<TProduct> GetEnumerator()
        {
            return products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TProduct prod)
        {
            if (products.Contains(prod))
            {
                Product pr = products.Where(p => p.Name == prod.Name).Single();
                return;
            }
            products.Add(prod);

        }
    }
}