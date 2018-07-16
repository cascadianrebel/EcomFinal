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
    public class AdminController : Controller
    {
        private IInventory _context;

        private readonly IConfiguration Configuration;

        public AdminController(IInventory context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
