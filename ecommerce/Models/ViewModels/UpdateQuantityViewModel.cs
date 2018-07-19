using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.ViewModels
{
    public class UpdateQuantityViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int BasketID { get; set; }
        public List<BasketItem> basketItems { get; set; }
    }
}
