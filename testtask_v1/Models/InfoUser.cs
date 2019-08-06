﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testtask_v1.Models
{
    public class InfoUser
    {
        [Required]
        public string Email
        {
            get;
            set;
        }

       
        public string Phone
        {
            get;
            set;
        }
    }
}