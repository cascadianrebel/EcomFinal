using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Finds current user logged in
        /// </summary>
        /// <returns> the user </returns>
        public async Task<ApplicationUser> CurrentUserAsync()
        {
            return await _userManager.FindByEmailAsync(User.Identity.Name);
        }

        // GET: /<controller>/
        /// <summary>
        /// The checkout page with the user information and current basket
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            CheckoutViewModel cvm = new CheckoutViewModel();
            var user = CurrentUserAsync();
            cvm.FirstName = user.Result.FirstName;
            cvm.LastName = user.Result.LastName;
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        /// <summary>
        /// Very similar to Index view. But takes in the CheckoutViewModel already filled in with the 
        /// user's inputted data from the Index View.
        /// </summary>
        /// <param name="cvm">CheckoutViewModel</param>
        /// <returns>The Update View</returns>
        [HttpPost]
        public IActionResult Update(CheckoutViewModel cvm)
        {
            var user = CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        /// <summary>
        /// All of their information inputted either in Index or Update
        /// User is unable to adjust information on this page
        /// </summary>
        /// <param name="cvm">CheckoutViewModel</param>
        /// <returns> review view</returns>
        [HttpPost]
        public IActionResult Review(CheckoutViewModel cvm)
        {
            var user = CurrentUserAsync();
            cvm.BasketItems = _context.GetAllBasketItem(user.Result.Id).Result;
            return View(cvm);
        }

        /// <summary>
        /// using Authrize.Net and SendGrid, we are able to process the user's checkout information and 
        /// send a email of their reciept. Closes out their basket and creates a new basket. Returns an 
        /// order summary view.
        /// </summary>
        /// <param name="cvm">CheckoutViewModel</param>
        /// <returns>Order summary view</returns>
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

            //Email Receipt Creation
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h2>Squirrels with Ties: Order Receipt</h2>");
            sb.AppendLine("<h3>Please see below for details for your recent purchase:</h3>");
            foreach (BasketItem item in cvm.BasketItems)
            {
                sb.AppendLine($"{item.Product.Name} <strong>QTY:</strong> {item.Quantity} <strong>Price:</strong> ${item.Product.Price} <br>");
            }
            sb.AppendLine($"<strong>Order Total:</strong> {total}");
            sb.Append("</p>");
            await _emailSender.SendEmailAsync(user.Email, "Your Recent Purchase", sb.ToString());


            CompleteBasket(cvm, user);
            MakeNewBasket(user);

            return View(cvm);
        }

        /// <summary>
        /// Changes the statud of the checked out basket to isComplete = true
        /// </summary>
        /// <param name="cvm"> CheckoutViewModel </param>
        /// <param name="user">the current user</param>
        public void CompleteBasket(CheckoutViewModel cvm, ApplicationUser user)
        {
            Basket basket = _context.GetCurrentBasket(user.Id).Result;
            basket.IsComplete = true;
            _context.UpdateBasket(basket);
        }

        /// <summary>
        /// Creates a new basket for the user
        /// </summary>
        /// <param name="user">the current user</param>
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
