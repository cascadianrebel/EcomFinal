using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.Interface
{
    public interface IOrder
    {
        Task<List<BasketItem>> GetAllBasketItem(string id);
        int GetBasketID(string id);
        void SaveOrder(Order order);
        Task<Basket> GetCurrentBasket(string id);
        void UpdateBasket(Basket basket);
        void AddBasket(Basket basket);
    }
}
