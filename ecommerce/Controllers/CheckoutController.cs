using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public CheckoutController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IOrder context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            _context = context;
            _emailSender = emailSender;
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

        public async Task<IActionResult> Summary(CheckoutViewModel cvm)
        {
            ApplicationUser user = await CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Id).Result;
            decimal total = 0;
            foreach (BasketItem item in cvm.BasketItems)
            {
                item.Product = _context.GetProduct(item.ProductID).Result;
                total += (item.Product.Price * item.Quantity);
            }

            Order myOrder = new Order
            {
                FirstName = cvm.FirstName,
                LastName = cvm.LastName,
                Address1 = cvm.Address1,
                Address2 = cvm.Address2,
                City = cvm.City,
                State = cvm.State,
                ZipCode = cvm.ZipCode,
                OrderDate = DateTime.Today,
                UserID = user.Id,
                BasketID = _context.GetBasketID(user.Id),
                Total = total,
                BasketItems = cvm.BasketItems,
            };

            if (cvm.CreditCard == CreditCard.AmericanExpress)
            {
                myOrder.CreditCard = "370000000000002";
            }
            if (cvm.CreditCard == CreditCard.Visa)
            {
                myOrder.CreditCard = "4111111111111111";
            }
            if (cvm.CreditCard == CreditCard.Mastercard)
            {
                myOrder.CreditCard = "5424000000000015";
            }

            _context.SaveOrder(myOrder);

            Payment payment = new Payment(Configuration);
            payment.RunPayment(myOrder);

            await _emailSender.SendEmailAsync(user.Email, "Your Order Receipt", "<p> Thank you for purchasing an item from Squirrels with ties </p>");

            CompleteBasket(cvm, user);
            MakeNewBasket(user);

            return View(cvm);
        }

        public void CompleteBasket(CheckoutViewModel cvm, ApplicationUser user)
        {
            Basket basket = _context.GetCurrentBasket(user.Id).Result;
            basket.IsComplete = true;
            _context.UpdateBasket(basket);
        }

        public void MakeNewBasket(ApplicationUser user)
        {
            Basket newBasket = new Basket
            {
                UserID = user.Id,
                IsComplete = false
            };
            _context.AddBasket(newBasket);
        }
    }
}
