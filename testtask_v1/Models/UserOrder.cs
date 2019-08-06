using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class UserOrder
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Email
        {
            get;
            set;
        }

        [Required]
        public string Phone
        {
            get;
            set;
        }
        
        public List<Product> Products
        {
            get;
            set;
        }
        
    }
}