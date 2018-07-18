using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    public class BasketController : Controller
    {
        private IBasket _context;

        private readonly IConfiguration Configuration;

        private readonly UserManager<ApplicationUser> _userManager;

        public BasketController(IBasket context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBasketItem(BasketItem bi)
        {
            if (bi != null)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                _context.AddToBasket(bi, user.Id);
            }
            return RedirectToAction("Inventory", "Shop");
        }
    }
}
