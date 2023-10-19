using HikeGroop.Interfaces;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly IPhotoService _photoService;

        public DestinationController(IDestinationRepository destinationRepository,
            IPhotoService photoService)
        {
            _destinationRepository = destinationRepository;
            _photoService = photoService;
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

        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDestinationViewModel destinationVM)
        {
            if (ModelState.IsValid)
            {
                var imageResult = await _photoService.AddPhotoAsync(destinationVM.Image);

                var newDestination = new Destination
                {
                    Title = destinationVM.Title,
                    Description = destinationVM.Description,
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    Itinerary = new Itinerary
                    {
                        MeetUp = destinationVM.Itinerary.MeetUp,
                        JumpOffPoint = destinationVM.Itinerary.JumpOffPoint,
                        Location = destinationVM.Itinerary.Location,
                        Elevation = destinationVM.Itinerary.Elevation,
                        DaysRequired = destinationVM.Itinerary.DaysRequired,

                    }

                };

                await _destinationRepository.Add(newDestination);
                return RedirectToAction("Index");
            }
            else
            {

                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(destinationVM);
        }
    }
}
