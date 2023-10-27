using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers;

public class DemoAccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public DemoAccountController(UserManager<AppUser> userManager,
     SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> DemoAdmin(DemoAdminViewModel demoAdminVM)
    {
        if (!ModelState.IsValid) return View("Error");
        var userLogin = await _userManager.FindByEmailAsync(demoAdminVM.EmailAddress);

        if (await UserExists(demoAdminVM.Username))
        {
            var password = await _userManager.CheckPasswordAsync(userLogin, demoAdminVM.Password);
            if (password)
            {
                var result = await _signInManager.PasswordSignInAsync(userLogin, demoAdminVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Group");
                }

            }
        }

        return View(demoAdminVM);
    }
    [HttpPost]
    public async Task<IActionResult> DemoUser(DemoUserViewModel demoUserVM)
    {
        if (!ModelState.IsValid) return View("Error");
        var userLogin = await _userManager.FindByEmailAsync(demoUserVM.EmailAddress);

        if (await UserExists(demoUserVM.Username))
        {
            var password = await _userManager.CheckPasswordAsync(userLogin, demoUserVM.Password);
            if (password)
            {
                var result = await _signInManager.PasswordSignInAsync(userLogin, demoUserVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Group");
                }
            }
        }

        return View(demoUserVM);
    }


    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(u => u.UserName == username);
    }
}
