using ecommerce.Data;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class DevInventory : IInventory
    {
        private EcomDbContext _context;

        public DevInventory(EcomDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
             _context.Products.Add(product);
             _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ID == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProduct()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> GetProductByID(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
