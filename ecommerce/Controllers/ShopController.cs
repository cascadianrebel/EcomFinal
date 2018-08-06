using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private IInventory _context;

        private readonly IConfiguration Configuration;

        public ShopController(IInventory context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        /// <summary>
        /// User facing side of all the products user can buy
        /// </summary>
        /// <returns>view</returns>
        [AllowAnonymous]
        public IActionResult Inventory()
        {
            var products = _context.GetProduct();
            return View(products);
        }

        /// <summary>
        /// A detail of the single product
        /// </summary>
        /// <param name="id">id of teh product</param>
        /// <returns>detial view or not found</returns>
        [AllowAnonymous]
        public IActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                if (id == 0)
                {
                    return NotFound();
                }
                BasketItem bi = new BasketItem();
                bi.Product = _context.GetProductByID(id.Value).Result;
                bi.ProductID = bi.Product.ID;
                if (bi.Product != null)
                {
                    return View(bi);
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
