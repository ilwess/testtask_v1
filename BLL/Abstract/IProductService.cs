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
        Task<IEnumerable<ProductDTO>> Get(params int[] productIds);
        Task EditAsync(int prodId, string newName,
            double newPrice, string newDescription);
        void ExportAll();
        void Export(string prodId);
        Task AddAsync(ProductDTO newProduct);
        Task DeleteAsync(int prodId);


    }
}
