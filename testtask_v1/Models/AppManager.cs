using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace testtask_v1.Models
{
    public class AppManager : UserManager<Customer>
    {
        public AppManager(IUserStore<Customer> store) : base(store){
        }

        public static AppManager Create(IdentityFactoryOptions<AppManager> options,
                                        IOwinContext context)
        {
            AppContext db = context.Get<AppContext>();
            AppManager manager = new AppManager(new UserStore<Customer>(db));
            return manager;
        }
    }
}