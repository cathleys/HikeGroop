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

        public AccountController(UserManager<AppUser> userManager)
        {
           _userManager = userManager;
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

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user == null)
            {
                TempData["Error"] = "Invalid username, Please try again"; 
                return View(loginViewModel);
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
           
            if(!isPasswordCorrect)
            {
                TempData["Error"] = "Invalid password, Please try again";
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Register()
        {
            var register = new RegisterViewModel();
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

           if(await UserExists(registerViewModel.Username)){

                TempData["Error"] = "Username is taken, please try again";
                return View(registerViewModel);

            }

            var newUser = new AppUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.EmailAddress
            };

            var newUserResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);
           
            if (newUserResult.Succeeded)
            {
            await _userManager.AddToRoleAsync(newUser, UserRoles.Member);
            return View("Login");
            }
            return View(registerViewModel);

        }


        private async Task<bool>UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
