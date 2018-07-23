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
    public class BasketPanel : ViewComponent
    {
        private EcomDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public BasketPanel (EcomDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = _context.BasketTable.FirstOrDefault(b => b.UserID == user.Id && b.IsComplete == false);
            var basketItems = await _context.BasketItemTable.Where(x => x.BasketID == basket.ID).ToListAsync();
            foreach (var item in basketItems)
            {
                item.Product = await _context.Products.FirstOrDefaultAsync(a => a.ID == item.ProductID);
            }

            return View(basketItems);
        }
    }
}
