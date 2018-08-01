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

        /// <summary>
        /// Returns the view where user can view all their products in their cart
        /// </summary>
        /// <returns>Index view</returns>
        public async Task<IActionResult> Index()
        {
            var user = await CurrentUserAsync();
            //List<BasketItem> basketItems = _context.GetAllBasketItem(user.Id).Result;
            UpdateQuantityViewModel ivc = new UpdateQuantityViewModel();
            ivc.basketItems = _context.GetAllBasketItem(user.Id).Result;
            return View(ivc);
        }

        /// <summary>
        /// Adds a product into the basket
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBasketItem(BasketItem bi)
        {
            if (bi != null)
            {
                var user = await CurrentUserAsync();
                _context.AddToBasket(bi, user.Id);
            }
            return RedirectToAction("Inventory", "Shop");
        }

        /// <summary>
        /// Finds the current user logged in
        /// </summary>
        /// <returns> the user </returns>
        public async Task<ApplicationUser> CurrentUserAsync()
        {
            return await _userManager.FindByEmailAsync(User.Identity.Name);
        }

        /// <summary>
        /// Deletes a product from the basket
        /// </summary>
        /// <param name="id">id of the basketItem</param>
        /// <returns>Index View or Not Found</returns>
        public IActionResult Delete(int? id)
        {
            if (id.HasValue && id != 0)
            {
                _context.RemoveBasketItem(id.Value);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        /// <summary>
        /// Takes the information from the forum in Index and updates the quantity of the basketitem to
        /// whatever they inputted
        /// </summary>
        /// <param name="ivc">UpdateQuantityViewModel</param>
        /// <returns>Index View or Not Found</returns>
        [HttpPost]
        public IActionResult UpdateQuantity(UpdateQuantityViewModel ivc)
        {
            if (ModelState.IsValid)
            {
                BasketItem basketItem = new BasketItem();
                basketItem.ID = ivc.ID;
                basketItem.ProductID = ivc.ProductID;
                basketItem.Quantity = ivc.Quantity;
                basketItem.BasketID = ivc.BasketID;
                _context.UpdateBasketItem(basketItem);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
