using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Components
{
    [Authorize]
    public class UserOrderPanel : ViewComponent
    {
        private EcomDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserOrderPanel(EcomDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var pastOrders = (from x in _context.OrderTable select x)
                .OrderBy(x => x.OrderDate);
            var userOrders = await pastOrders.Where(x => x.UserID == user.Id).Take(5).ToListAsync();


            foreach (Order k in userOrders)
            {
                Basket basket = await _context.BasketTable.FirstOrDefaultAsync(b => b.ID == k.BasketID);
                var basketItems = await _context.BasketItemTable.Where(x => x.BasketID == basket.ID).ToListAsync();

                foreach (var x in basketItems)
                {
                    x.Product = await _context.Products.FindAsync(x.ProductID);
                }
            }


            return View(userOrders);
        }
    }
}
