using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class RegisterUser
    {
        [Required]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords isn't equals")]
        [DataType(DataType.Password)]
        public string PasswordConf { get; set; }
    }
}