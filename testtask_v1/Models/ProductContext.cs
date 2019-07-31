using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace testtask_v1.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Prods { get; set; }
    }
}