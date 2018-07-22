using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IOrder _context;
        private readonly IConfiguration Configuration;

        public CheckoutController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IOrder context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            _context = context;
        }

        public async Task<ApplicationUser> CurrentUserAsync()
        {
            return await _userManager.FindByEmailAsync(User.Identity.Name);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            CheckoutViewModel cvm = new CheckoutViewModel();
            var user = CurrentUserAsync();
            cvm.FirstName = user.Result.FirstName;
            cvm.LastName = user.Result.LastName;
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Update(CheckoutViewModel cvm)
        {
            var user = CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Review(CheckoutViewModel cvm)
        {
            var user = CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        public IActionResult Summary(CheckoutViewModel cvm)
        {
            var user = CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }
    }
}
