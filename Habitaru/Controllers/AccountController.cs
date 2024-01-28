using Azure.Identity;
using Habitaru.AutoMapperConfig;
using Habitaru.Migrations;
using Habitaru.Models;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Habitaru.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var allUsers = _userManager.Users.AsEnumerable();
            return View(allUsers);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Registeration");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserRegisterationVM newUser)
        {
            if (ModelState.IsValid)
            {
                var mapper = AutomapperConfig.InitializeAutoMapper();
                var user = mapper.Map<ApplicationUser>(newUser);

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Habit");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Registeration",newUser);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(UserLoginVM newUser)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(newUser.Username);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "username is invalid");
                    return RedirectToAction("Login", newUser);
                }
                // there is an error here 
                var result = await _userManager.CheckPasswordAsync(appUser, newUser.Password);
                if (result == false)
                {
                    ModelState.AddModelError("", "password is invalid");
                    return RedirectToAction("Login", newUser);
                }

                await _signInManager.SignInAsync(appUser, newUser.RememberMe);
                return RedirectToAction("Index", "Habit");
            }
            ModelState.AddModelError("", "username and password are invalid");
            return RedirectToAction("Login", newUser);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
