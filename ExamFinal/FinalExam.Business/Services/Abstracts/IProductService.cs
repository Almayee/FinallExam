using FinalExam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Abstracts
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        void UpdateProduct(Product newProduct,int id);
        void DeleteProduct(int id);
        Product GetProduct(Func<Product,bool>? predicate=null);
        List<Product> GetAllProducts(Func<Product, bool>? predicate=null);
    }
}
