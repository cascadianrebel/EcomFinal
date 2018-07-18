using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var basket = _context.BasketTable.FirstOrDefault(b => b.UserID == user.Id);
            return View(basket);
        }
    }
}
