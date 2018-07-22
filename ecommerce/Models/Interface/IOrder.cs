using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.Interface
{
    public interface IOrder
    {
        Task<List<BasketItem>> GetAllBasketItem(string id);
    }
}
