using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ecommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IInventory _context;

        private readonly IConfiguration Configuration;

        public HomeController(IInventory context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}