using CloudinaryDotNet.Actions;
using HikeGroop.Data;
using HikeGroop.Extensions;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HikeGroop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository,
        IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }


        public async Task<IActionResult> Index()
        {
            var userGroups = await _dashboardRepository.GetUserGroups();
            var userDestinations = await _dashboardRepository.GetUserDestinations();

            var dashboardViewModel = new DashboardViewModel
            {
                Groups = userGroups,
                Destinations = userDestinations
            };

            return View(dashboardViewModel);

        }


        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null) return View("Error");

            var editUserDashboardViewModel = new EditUserDashboardViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,

            };
            return View(editUserDashboardViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editUserViewModel);
            }

            var user = await _dashboardRepository.GetUserByIdNoTracking(editUserViewModel.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var imageUploadResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);

                MapUserEdit(user, editUserViewModel, imageUploadResult);
                await _dashboardRepository.Update(user);

                return RedirectToAction("Detail", "User", new { user.Id });

            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);

                }
                catch (System.Exception)
                {

                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(editUserViewModel);
                }

                var imageUploadResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);
                MapUserEdit(user, editUserViewModel, imageUploadResult);
                await _dashboardRepository.Update(user);

                return RedirectToAction("Detail", "User", new { user.Id });
            }

        }

        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM,
        ImageUploadResult imageUploadResult)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.Username;
            user.City = editVM.City;
            user.HikerType = editVM.HikerType;
            user.ProfileImageUrl = imageUploadResult.SecureUrl.AbsoluteUri;

        }
    }
}