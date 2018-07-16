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
    [Authorize(Policy = "AdminOnly")]
    public class ProductController : Controller
    {
        private IInventory _context;

        private readonly IConfiguration Configuration;

        public ProductController(IInventory context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Inventory()
        {
            var products = _context.GetProduct();
            return View(products);
        }

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

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id.HasValue && id != 0)
            {
                Product product = _context.GetProductByID(id.Value).Result;
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            _context.UpdateProduct(product);
            //return RedirectToAction("Detail", "Admin", (int?)product.ID);
            return RedirectToAction("Inventory", "Admin");
        }

        public IActionResult Delete(int? id)
        {
            if (id.HasValue && id != 0)
            {
                _context.Delete(id.Value);
                return RedirectToAction("Inventory", "Admin");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.CreateProduct(product);
            return RedirectToAction("Inventory", "Admin");
        }
    }
}
