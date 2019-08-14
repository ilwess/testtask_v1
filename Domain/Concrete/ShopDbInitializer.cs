using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Concrete;

namespace testtask_v1.Models
{
    public class ShopDbInitializer : CreateDatabaseIfNotExists<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            base.Seed(context);
        }
    }
}