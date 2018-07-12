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
        Task<IActionResult> CreateProduct(Product product);
        Task<IActionResult> GetProductByID(int id);
        Task<IActionResult> GetProduct();
        Task<IActionResult> UpdateProduct(int id, Product product);
        Task<IActionResult> Delete(int id);
    }
}
