using ecommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.Interface
{
    public interface IBasket
    {
        void AddToBasket(BasketItem bi, string id);
        Task<BasketItem> GetOneBasketItem(int id);
        Task<List<BasketItem>> GetAllBasketItem(string id);
        void UpdateBasketItem(BasketItem bi);
        void RemoveBasketItem(int id);
        void AddBasket(Basket basket);
    }
}
