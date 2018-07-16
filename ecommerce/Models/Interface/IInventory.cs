using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;

namespace ecommerce.Models.Interface
{
    public interface IInventory
    {
        void CreateProduct(Product product);
        Task<Product> GetProductByID(int id);
        IEnumerable<Product> GetProduct();
        void UpdateProduct(Product product);
        void Delete(int id);
    }
}
