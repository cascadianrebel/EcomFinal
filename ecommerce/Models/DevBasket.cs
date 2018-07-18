using ecommerce.Data;
using ecommerce.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class DevBasket : IBasket
    {
        private EcomDbContext _context;

        public DevBasket(EcomDbContext context)
        {
            _context = context;
        }

        public async void AddBasket(Basket basket)
        {
            await _context.BasketTable.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async void AddToBasket(BasketItem bi, string id)
        {
            var basket = _context.BasketTable.FirstOrDefault(b => b.UserID == id && b.IsComplete == false);
            bi.BasketID = basket.ID;
            BasketItem basketItem = _context.BasketItemTable.FirstOrDefault(b =>
                b.ProductID == bi.ProductID && b.BasketID == bi.BasketID);
            if (basketItem != null)
            {
                basketItem.Quantity += bi.Quantity;
                _context.BasketItemTable.Update(basketItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.BasketItemTable.Add(bi);
                _context.SaveChanges();
            }
        }

        public Task<BasketItem> GetOneBasketItem(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveBasketItem(int id)
        {
            BasketItem basketItem = _context.BasketItemTable.Find(id);
            _context.BasketItemTable.Remove(basketItem);
            _context.SaveChanges();
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            throw new NotImplementedException();
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
