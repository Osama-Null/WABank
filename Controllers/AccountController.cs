using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WABank.Models;
using WABank.Models.ViewModels;

namespace WABank.Controllers
{
    public class AccountController : Controller
    {
        #region InjectedServices
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> SignInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = SignInManager;
            _roleManager = roleManager;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterLogin()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles, "RoleId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterLoginViewModel model)
        {
            ModelState.Remove("LoginVM");
            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState);
                ViewBag.Roles = new SelectList(_roleManager.Roles, "RoleId", "Name");
                return View("RegisterLogin", model);
            }

            AppUser user = new AppUser
            {
                UserName = model.RegisterVM.Email,
                Email = model.RegisterVM.Email,
                PhoneNumber = model.RegisterVM.Mobile,
            };
            var result = await _userManager.CreateAsync(user, model.RegisterVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View("RegisterLogin", model);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("RegisterLogin");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
