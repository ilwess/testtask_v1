using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Concrete;
using Domain.Entities;

namespace testtask_v1.Models
{
    public class ShopDbInitializer : CreateDatabaseIfNotExists<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            context.Products.Add(
                new Product() { Name = "Ball", Description = "big ball", Price = 3 });
            context.Products.Add(
                new Product() { Name = "Car", Description = "small car", Price = 2 });
            context.Products.Add(
                new Product() { Name = "Sword", Description = "katana", Price = 5 });
            context.Products.Add(
                new Product() { Name = "Cup", Description = "golden cup", Price = 5 });

            base.Seed(context);
        }
    }
}