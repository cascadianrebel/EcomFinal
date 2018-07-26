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
            var pastOrders = await (from x in _context.OrderTable select x).Take(20)
                .OrderBy(x=> x.OrderDate).ToListAsync();

            return View(pastOrders);
        }
    }
}
