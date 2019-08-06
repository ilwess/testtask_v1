using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace testtask_v1.Models
{
    public class AppContext : IdentityDbContext<Customer>
    {
        public AppContext() : base("IdentityDb")
        {
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}