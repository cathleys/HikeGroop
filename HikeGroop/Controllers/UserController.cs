using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HikeGroop.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index(PaginationParams paginationParams,
        string searchString)
        {
            var users = await _uow.UserRepository.GetMembers(paginationParams, searchString);
            return View(users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _uow.UserRepository.GetUserById(id);

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