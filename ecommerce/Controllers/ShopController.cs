using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
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

        [AllowAnonymous]
        public IActionResult Inventory()
        {
            var products = _context.GetProduct();
            return View(products);
        }
        [AllowAnonymous]
        public IActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                if (id == 0)
                {
                    return NotFound();
                }
                Product product = _context.GetProductByID(id.Value).Result;
                if (product != null)
                {
                    return View(product);
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
