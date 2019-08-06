using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class PhoneCustomer
    {

        [Required]
        public string CountryCode
        {
            get;
            set;
        }

        [Required]
        public string OperatorCode
        {
            get;
            set;
        }

        [Required]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Required]
        public string FullPhoneNumber
        {
            get { return CountryCode + OperatorCode + PhoneNumber; }
        }
    }
}