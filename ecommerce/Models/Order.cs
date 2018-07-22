using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int BasketID { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
