using HikeGroop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var dests = await _destinationRepository.GetDestinations();
            return View(dests);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var dest = await _destinationRepository.GetDestinationByIdAsync(id);
            return View(dest);
        }
    }
}
