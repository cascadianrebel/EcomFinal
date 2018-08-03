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

        public int GetBasketID(string id)
        {
            Basket basket = _context.BasketTable.FirstOrDefault(b => b.UserID == id && b.IsComplete == false);
            return basket.ID;
        }

        public async void SaveOrder(Order order)
        {
            _context.OrderTable.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Basket> GetCurrentBasket(string id)
        {
            Basket basket = await _context.BasketTable.FirstOrDefaultAsync(b => b.UserID == id && b.IsComplete == false);
            return basket;
        }

        public void UpdateBasket(Basket basket)
        {
            _context.BasketTable.Update(basket);
        }

        public async Task AddBasket(Basket basket)
        {
            await _context.BasketTable.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
