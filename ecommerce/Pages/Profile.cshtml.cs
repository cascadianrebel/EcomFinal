using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecommerce.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public ProfileViewModel ProfileInfo { get; set; }

        public ProfileModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            ProfileInfo = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            user.FirstName = ProfileInfo.FirstName;
            user.LastName = ProfileInfo.LastName;

            Claim claim = User.Claims.First(c => c.Type == "FullName");
            Claim nameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");

            await _userManager.RemoveClaimAsync(user, claim);
            await _userManager.AddClaimAsync(user, nameClaim);

            await _userManager.UpdateAsync(user);

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
    }
}