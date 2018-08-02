using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private IInventory _context;
        private EcomDbContext _order;

        private readonly IConfiguration Configuration;

        public AdminController(IInventory context, IConfiguration configuration, EcomDbContext order)
        {
            _context = context;
            Configuration = configuration;
            _order = order;
        }

        public async Task<IActionResult> Order()
        {
            
            List<Order> orders = await _order.OrderTable.OrderByDescending(o => o.OrderDate).Take(20).ToListAsync();
            foreach (var order in orders)
            {
                var basket = await _order.BasketTable.FirstOrDefaultAsync(x => x.ID == order.BasketID);
                order.BasketItems = await _order.BasketItemTable.Where(c => c.BasketID == basket.ID).ToListAsync();
                foreach (var i in order.BasketItems)
                {
                    i.Product = await _order.Products.FirstOrDefaultAsync(x => x.ID == i.ProductID);
                }
            }
            return View (orders);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
