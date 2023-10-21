using HikeGroop.Data;
using HikeGroop.Interfaces;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HikeGroop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
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
    }
}