using HikeGroop.Data;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var login = new LoginViewModel();
            return View(login);//preserving data when user loads page.
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var userLogin = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (userLogin != null)
            {
                var password = await _userManager.CheckPasswordAsync(userLogin, loginViewModel.Password);
                if (password)
                {
                    var result = await _signInManager.PasswordSignInAsync(userLogin, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Group");
                    }

                }
                TempData["Error"] = "Invalid password, Please try again";
                return View(loginViewModel);
            }
            TempData["Error"] = "User doesn't exist, please try again";
            return View(loginViewModel);
        }
        public IActionResult Register()
        {
            var register = new RegisterViewModel();
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            if (await UserExists(registerViewModel.Username))
            {

                TempData["Error"] = "Username is taken, please try again";
                return View(registerViewModel);

            }

            var newUser = new AppUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.EmailAddress,
                HikerType = registerViewModel.HikerType,
                City = registerViewModel.City
            };

            var newUserResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Member);
                return View("Login");
            }
            return View(registerViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
