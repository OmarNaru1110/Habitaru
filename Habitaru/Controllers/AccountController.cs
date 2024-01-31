using Azure.Identity;
using Habitaru.AutoMapperConfig;
using Habitaru.Migrations;
using Habitaru.Models;
using Habitaru.Services.IServices;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Habitaru.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles ="Admin")]
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
                    var role = await _roleManager.FindByNameAsync("Normal User");
                    await _userManager.AddToRoleAsync(user, role.Name);
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
        public IActionResult RegisterUserInRole()
        {
            ViewBag.roles = new SelectList(_roleManager.Roles, "Id", "Name");

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterUserInRole(UserRegisterationVM newUser)
        {
            if (ModelState.IsValid)
            {
                var mapper = AutomapperConfig.InitializeAutoMapper();
                var user = mapper.Map<ApplicationUser>(newUser);

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(newUser.RoleId.ToString());
                    var RoleAddedRes = await _userManager.AddToRoleAsync(user,role.Name);
                    if(RoleAddedRes.Succeeded)
                        return Ok("Role Added");
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
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
        [HttpGet]
        public async Task<IActionResult> EditUserName()
        {
            if(User.Identity.IsAuthenticated==false)
                return RedirectToAction(nameof(Login));
            var appUser = await _userManager.GetUserAsync(User);
            if(appUser == null)
                return RedirectToAction(nameof(Login));

            var userVM = appUser.UserName;

            return View(model:userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserName(string? username)
        {
            if (username == null)
            {
                ModelState.AddModelError("", "please fill the field");
                return View();
            }
            var savedUser = await _userManager.GetUserAsync(User);

            savedUser.UserName = username;

            var result = await _userManager.UpdateAsync(savedUser);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(savedUser);
                return RedirectToAction("index", "Habit");
            }
            ModelState.AddModelError("", "username already exists");
            return View(model:username);
        }
        [HttpGet]
        public async Task<IActionResult> EditPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(EditPasswordVM? password)
        {
            if(ModelState.IsValid == false)
            {
                foreach(var item in ModelState.Values)
                {
                    foreach(var error in item.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View();
            }
            var savedUser = await _userManager.GetUserAsync(User);
            if(savedUser == null)
            {
                ModelState.AddModelError("", "try again");
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(savedUser, password.OldPassword, password.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("index", "Habit");
            
            return View();
        }
    }
}
