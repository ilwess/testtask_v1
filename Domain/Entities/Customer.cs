using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class Customer
    {
        [Required]
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

        public string PhoneNumber
        {
            get;
            set;
        }

        public Customer()
        {

        }

        public Customer(string email, string phoneNumber = "")
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }


    }
}