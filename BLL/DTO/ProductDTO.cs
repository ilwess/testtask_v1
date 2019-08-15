using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class ProductDTO
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public virtual List<OrderDTO> Orders
        {
            get;
            set;
        }

        public ProductDTO()
        {

        }
    }
}
