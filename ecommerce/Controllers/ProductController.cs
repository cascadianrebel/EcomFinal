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

        /// <summary>
        /// All of the products the site sells
        /// </summary>
        /// <returns>Inventory View</returns>
        public IActionResult Inventory()
        {
            var products = _context.GetProduct();
            return View(products);
        }

        /// <summary>
        /// The detail page of one specific product
        /// </summary>
        /// <param name="id">the id of the product </param>
        /// <returns> detail view or not found</returns>
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

        /// <summary>
        /// Sends the forum of the product to update the information of the product
        /// </summary>
        /// <param name="id">the id</param>
        /// <returns>not found or update view</returns>
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

        /// <summary>
        /// grabs the information entered by admin and saves the new product information to the DB
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Inventory of AdminController</returns>
        [HttpPost]
        public IActionResult Update(Product product)
        {
            _context.UpdateProduct(product);
            //return RedirectToAction("Detail", "Admin", (int?)product.ID);
            return RedirectToAction("Inventory", "Admin");
        }

        /// <summary>
        /// Deletes a specific product from the DB
        /// </summary>
        /// <param name="id">the id of the product</param>
        /// <returns> Inventory Action of Admin Controller or Not found</returns>
        public IActionResult Delete(int? id)
        {
            if (id.HasValue && id != 0)
            {
                _context.Delete(id.Value);
                return RedirectToAction("Inventory", "Admin");
            }
            return NotFound();
        }

        /// <summary>
        /// Add Product forum
        /// </summary>
        /// <returns>add product view</returns>
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        /// <summary>
        /// takes the information inputted by admin and saves it in the DB
        /// </summary>
        /// <param name="product">the bew product </param>
        /// <returns>Inventory action of admin controller</returns>
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.CreateProduct(product);
            return RedirectToAction("Inventory", "Admin");
        }
    }
}
