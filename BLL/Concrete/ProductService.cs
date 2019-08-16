using AutoMapper;
using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL.Concrete
{
    public class ProductService : IProductService
    {
        IUnitOfWork db;

        public ProductService(IUnitOfWork uow)
        {
            db = uow;
        }

        public async Task AddAsync(ProductDTO newProduct)
        {
            Product prod = new Product()
            {
                Name = newProduct.Name,
                Orders = null,
                Description = newProduct.Description,
                Price = newProduct.Price,
            };
            db.Products.Add(prod);
            await db.CommitAsync();
        }

        public void Export(string prodName)
        {
            Product productToExport = db
                .Products
                .Get(p => p.Name == prodName)
                .FirstOrDefault();
            XmlSerializer formatter = new XmlSerializer(typeof(Product));
            using (FileStream fs = new FileStream(@"C:\Users\Админ\Documents\Visual Studio 2017\Projects\testtask_v1\testtask_v1\Products\Product\" + "Product" + productToExport.Id + ".xml", FileMode.Create))
            {
                formatter.Serialize(fs, productToExport);
            }
        }

        public void ExportAll()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Product>));
            using (FileStream fs
                = new FileStream(
                    @"C:\Users\Админ\Documents\Visual Studio 2017\Projects\testtask_v1\testtask_v1\Products\Products.xml",
                    FileMode.Create))
            {
                formatter.Serialize(
                    fs,
                    db.Products.Get());
            }
        }

        public IEnumerable<ProductDTO> Get(Func<Product, bool> predicate)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDTO>())
                .CreateMapper();
            return mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(
                db.Products.Get(predicate));
        }

        public async Task<IEnumerable<ProductDTO>> Get(params int[] productIds)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return await mapper
                .Map<Task<IEnumerable<Product>>, Task<IEnumerable<ProductDTO>>>(
                db.Products.FindRangeAsync(productIds));
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDTO>())
                .CreateMapper();
            return mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(
                db.Products.Get());
        }

        public async Task DeleteAsync(int prodId)
        {
            Product prodToDel = db.Products.Get(p => p.Id == prodId)
                .FirstOrDefault();
            db.Products.Remove(prodToDel);
            await db.CommitAsync();
        }

        public async Task EditAsync(int prodId, string newName, double newPrice, string newDescription)
        {
            Product prodToEdit = await db.Products.FindAsync(prodId);
            prodToEdit.Name = newName;
            prodToEdit.Price = newPrice;
            prodToEdit.Description = newDescription;

            db.Products.Update(prodToEdit);
        }
    }
}
