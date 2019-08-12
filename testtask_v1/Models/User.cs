using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

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
    }

}