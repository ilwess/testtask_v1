using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class OrderDTO
    {

        public int Id
        {
            get;
            set;
        }

        public virtual CustomerDTO Orderer { get; set; }

        public virtual List<ProductDTO> Products
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

    }
}
