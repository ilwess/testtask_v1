using Owin;
using testtask_v1.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace testtask_v1
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<Models.AppContext>(Models.AppContext.Create);
            app.CreatePerOwinContext<AppManager>(AppManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);



            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator
                    .OnValidateIdentity<AppManager, User>(
                        validateInterval: TimeSpan.FromHours(3),
                        regenerateIdentity: (manager, user) =>
                        user.GenerateUserIdentityAsync(manager)),
                }
            });
        }
    }
}