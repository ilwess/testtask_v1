﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class CustomerDTO
    {
        public int Id
        {
            get;
            set;
        }

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

        public CustomerDTO()
        {

        }
    }
}
