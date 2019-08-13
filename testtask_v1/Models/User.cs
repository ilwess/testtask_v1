using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace testtask_v1.Models
{
    
    public class User : IdentityUser
    {
        public DateTime RegistrationDate
        {
            get;
            set;
        }

        public User()
        {
            RegistrationDate = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

}