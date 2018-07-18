using ecommerce.Data;
using ecommerce.Models.Interface;
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
            await _context.BasketItemTable.AddAsync(bi);
            await _context.SaveChangesAsync();
        }

        public Task<BasketItem> GetOneBasketItem(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveBasketItem(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        IEnumerable<BasketItem> IBasket.GetAllBasketItem()
        {
            throw new NotImplementedException();
        }
    }
}
