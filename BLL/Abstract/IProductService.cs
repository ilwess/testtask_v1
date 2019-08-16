using BLL.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAll();
        IEnumerable<ProductDTO> Get(Func<Product, bool> predicate);
        void ExportAll();
        void Export(int prodId);
        void Add(ProductDTO newProduct);
    }
}
