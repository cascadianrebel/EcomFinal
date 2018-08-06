using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Components
{
    public class AdminOrderPanel : ViewComponent
    {
        private EcomDbContext _context;
        
        public AdminOrderPanel(EcomDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pastOrders = await (from x in _context.OrderTable select x)
                .OrderBy(x=> x.OrderDate).Take(20).ToListAsync();


            foreach(Order k in pastOrders)
            {
                Basket basket = await _context.BasketTable.FirstOrDefaultAsync(b => b.ID == k.BasketID);    
                var basketItems = await _context.BasketItemTable.Where(x => x.BasketID == basket.ID).ToListAsync();
  
                foreach(var x in basketItems)
                {
                    x.Product = await _context.Products.FindAsync(x.ProductID);
                }
            }

            return View(pastOrders);
        }
    }
}
