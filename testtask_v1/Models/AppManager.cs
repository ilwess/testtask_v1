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
    public class AppManager : UserManager<User>
    {
        public AppManager(IUserStore<User> store) : base(store){
        }

        public static AppManager Create(IdentityFactoryOptions<AppManager> options,
                                        IOwinContext context)
        {
            AppContext db = context.Get<AppContext>();
            AppManager manager = new AppManager(new UserStore<User>(db));
            RoleManager<AppRole> rm = new RoleManager<AppRole>(new RoleStore<AppRole>(db));
            AppRole userRole = new AppRole()
            {
                Name = "User",
            };
            AppRole adminRole = new AppRole()
            {
                Name = "Admin",
            };
            AppRole managerRole = new AppRole()
            {
                Name = "Manager",
            };
            rm.Create(userRole);
            rm.Create(adminRole);
            rm.Create(managerRole);
            manager.EmailService = new IdentityEmailService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if(dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(
                        dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromMinutes(2),
                    };

            }
            return manager;
        }
    }
}