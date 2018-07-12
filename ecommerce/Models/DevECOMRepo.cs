using ecommerce.Data;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class DevECOMRepo : IInventory
    {
        private EcomDbContext _context;

        public DevECOMRepo(EcomDbContext context)
        {
            _context = context;
        }

        public Task<IActionResult> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProduct()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
