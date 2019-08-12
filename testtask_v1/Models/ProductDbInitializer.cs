using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace testtask_v1.Models
{
    public class ProductDbInitializer : CreateDatabaseIfNotExists<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            base.Seed(context);
        }
    }
}