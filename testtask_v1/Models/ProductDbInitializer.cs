using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace testtask_v1.Models
{
    public class ProductDbInitializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            context.Prods.Add(new Product("Pen", 1, "good pen)"));
            context.Prods.Add(new Product("Ball", 5, "the best ball ))"));
            context.Prods.Add(new Product("Plastic left leg", 10, "pretty good plastic leg for people without left leg"));
            context.Prods.Add(new Product("Plastic right leg", 10, "pretty good plastic right leg for people without right leg"));
            context.Prods.Add(new Product("Right and left plastic legs", 19, "nice set of legs with discount"));
            base.Seed(context);
        }
    }
}