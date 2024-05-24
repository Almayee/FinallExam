using FinalExam.Business.Exceptions;
using FinalExam.Business.Extensions;
using FinalExam.Business.Services.Abstracts;
using FinalExam.Core.Models;
using FinalExam.Core.RepositoryAabstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _env;
        public ProductService(IProductRepository productRepository, IWebHostEnvironment env)
        {
            _repository= productRepository;
            _env= env;
        }
        public async Task AddProduct(Product product)
        {
            if (product.ImageFile == null)
                throw new Exceptions.FileNotFoundException("File bos ola bilmez!");

            product.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads/products", product.ImageFile);
            await _repository.AddAsync(product);
            await _repository.CommitAsync();
        }

        public void DeleteProduct(int id)
        {
           var existProduct=_repository.Get(x=> x.Id == id);
            if (existProduct == null)
                throw new EntityNotFoundException("Product tapilmadi!");
            Helper.DeleteFile(_env.WebRootPath, @"uploads\products", existProduct.ImageUrl);

           _repository.Delete(existProduct);
            _repository.Commit();
        }

        public List<Product> GetAllProducts(Func<Product, bool>? predicate = null)
        {
             return _repository.GetAll(predicate);
        }

        public Product GetProduct(Func<Product, bool>? predicate = null)
        {
            return _repository.Get(predicate);
        }

        public void UpdateProduct(Product newProduct, int id)
        {
            var oldProduct = _repository.Get(x => x.Id == id);

            if (oldProduct == null)
                throw new EntityNotFoundException("Product tapilmadi!");

            if (newProduct.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\products", oldProduct.ImageUrl);
                oldProduct.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\products", newProduct.ImageFile);
            }
           


            _repository.Commit();
        }
    }
}
