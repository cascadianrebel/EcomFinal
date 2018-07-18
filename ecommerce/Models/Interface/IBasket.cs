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
        IEnumerable<BasketItem> GetAllBasketItem();
        void UpdateBasketItem(BasketItem basketItem);
        void RemoveBasketItem(int id);
        void AddBasket(Basket basket);
    }
}
