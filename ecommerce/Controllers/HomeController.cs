using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private IInventory _context;

        private readonly IConfiguration Configuration;

        public HomeController(IInventory context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }


        public IActionResult Index()
        {
            return View();
        }
    }
}