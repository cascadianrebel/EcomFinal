using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class Basket
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public bool IsComplete { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
