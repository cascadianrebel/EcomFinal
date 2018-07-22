using ecommerce.Data;
using ecommerce.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class DevOrder : IOrder
    {
        private EcomDbContext _context;

        public DevOrder(EcomDbContext context)
        {
            _context = context;
        }

        public async Task<List<BasketItem>> GetAllBasketItem(string id)
        {
            Basket basket = _context.BasketTable.FirstOrDefault(b => b.UserID == id && b.IsComplete == false);
            var basketItems = await _context.BasketItemTable.Where(bi => bi.BasketID == basket.ID).ToListAsync();
            foreach (BasketItem item in basketItems)
            {
                item.Product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
            }
            return basketItems;
        }
    }
}
