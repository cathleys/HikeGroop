using cloudscribe.Pagination.Models;
using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace HikeGroop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index(PaginationParams paginationParams)
        {
            var users = await _userRepository.GetMembers(paginationParams);
            return View(users);
        }
        public async Task<IActionResult> Index2()
        {
            var users = await _userRepository.GetAllUsers();
            var result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                    HikerType = user.HikerType,
                    City = user.City

                };

                result.Add(userViewModel);
            }

            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return View("Error");

            var userDetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
                HikerType = user.HikerType,
                City = user.City
            };

            return View(userDetailViewModel);

        }

    }
}