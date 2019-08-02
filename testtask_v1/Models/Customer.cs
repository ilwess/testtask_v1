using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;

namespace testtask_v1.Models
{
    public class Customer
    {
        public int Id
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public DateTime RegDate
        {
            get;
            set;
        }
    }
}