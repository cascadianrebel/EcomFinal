﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.Interface;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ecommerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IBasket _context;
        private readonly IConfiguration Configuration;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IBasket context, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
            Configuration = configuration;
        }

        

        /// <summary>
        /// Show the Register View
        /// </summary>
        /// <returns>The Register view </returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// adds new user to user database and creates claims with the respective information
        /// </summary>
        /// <param name="rvm"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            //if (the forms are filled correctly)
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Birthday = rvm.Birthday,
                    FavoriteAnimal = rvm.FavoriteAnimal
                };
                //inserts user data into userdb
                var result = await _userManager.CreateAsync(user, rvm.Password);

                //after user data successfully placed in db
                if (result.Succeeded)
                {
                    //create a list of claims called claims
                    List<Claim> claims = new List<Claim>();


                    // create new claims
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    Claim animalClaim = new Claim("FavAnimal", $"{user.FavoriteAnimal}");


                    //add claims to claims list
                    claims.Add(nameClaim);
                    claims.Add(emailClaim);
                    claims.Add(animalClaim);

                    await _userManager.AddClaimsAsync(user, claims);

                    // Instantiates a new basket
                    Basket basket = new Basket();
                    //Sets basket's userID as id of user
                    basket.UserID = user.Id;
                    //Dependency Injection to IBasket 
                    //There we will add the basket to the BasketTable
                    //AddToBasket and AddBasket are different
                    _context.AddBasket(basket);

                    if (user.Email == "admin@agmn.org")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Admin");
                    }
                    await _signInManager.SignInAsync(user, false);
                    await _emailSender.SendEmailAsync(user.Email, "Welcome", "<p> You have successfully registered </p>");
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    return RedirectToAction("Index", "Home");
                }


            }
            return View(rvm);
        }

        /// <summary>
        /// Shows the login page
        /// </summary>
        /// <returns>the login view</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        /// <summary>
        /// Takes the information from the login view forum and checks the DB. If it exists logs in the user
        /// </summary>
        /// <param name="lvm">LoginViewModel</param>
        /// <returns>The same login view or home index or admin index</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);
            var user = await _userManager.FindByEmailAsync(lvm.Email);
            if (ModelState.IsValid)
            {
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Either your email or password was incorrect");
                }
            }
           
            return View(lvm);
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        /// <returns>Home index</returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LoggedOut"] = "User Logged Out";

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Starts the external login/resgiter process
        /// </summary>
        /// <param name="provider"> a string of the provider</param>
        /// <returns> a challenge </returns>
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData["ErrorMessage"] = "Error from Provider";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            
            return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel elvm)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    TempData["Error"] = "Error loading information";
                }

                var user = new ApplicationUser
                {
                    UserName = elvm.Email,
                    Email = elvm.Email,
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    Birthday = elvm.Birthday,
                    FavoriteAnimal = elvm.FavoriteAnimal
                };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    //create a list of claims called claims
                    List<Claim> claims = new List<Claim>();


                    // create new claims
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    Claim animalClaim = new Claim("FavAnimal", $"{user.FavoriteAnimal}");


                    //add claims to claims list
                    claims.Add(nameClaim);
                    claims.Add(emailClaim);
                    claims.Add(animalClaim);

                    await _userManager.AddClaimsAsync(user, claims);

                    // Instantiates a new basket
                    Basket basket = new Basket();
                    //Sets basket's userID as id of user
                    basket.UserID = user.Id;
                    //Dependency Injection to IBasket 
                    //There we will add the basket to the BasketTable
                    //AddToBasket and AddBasket are different
                    _context.AddBasket(basket);
                    result = await _userManager.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("ExternalLoginCallback", "Account", elvm);
        }
    }
}