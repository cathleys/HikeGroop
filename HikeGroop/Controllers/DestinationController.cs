using HikeGroop.Data.Enums;
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
        public async Task<IActionResult> Create(CreateDestinationViewModel editDestViewModel)
        {
            if (ModelState.IsValid)
            {
                var imageResult = await _photoService.AddPhotoAsync(editDestViewModel.Image);

                var newDestination = new Destination
                {
                    Title = editDestViewModel.Title,
                    Description = editDestViewModel.Description,
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    HikingCategory = editDestViewModel.HikingCategory,
                    HikingTour = editDestViewModel.HikingTour,
                    TrailClass = editDestViewModel.TrailClass,
                    Itinerary = new Itinerary
                    {
                        MeetUp = editDestViewModel.Itinerary.MeetUp,
                        JumpOffPoint = editDestViewModel.Itinerary.JumpOffPoint,
                        Location = editDestViewModel.Itinerary.Location,
                        Elevation = editDestViewModel.Itinerary.Elevation,
                        DaysRequired = editDestViewModel.Itinerary.DaysRequired,

                    }

                };

                await _destinationRepository.Add(newDestination);
                return RedirectToAction("Index");
            }
            else
            {

                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(editDestViewModel);
        }


        
        public async Task<IActionResult> Edit(int id)
        {

            var dest = await _destinationRepository.GetDestinationByIdAsync(id);

            if (dest == null) return View("Error");

            var destVM = new EditDestinationViewModel
            {
                Title = dest.Title,
                Description = dest.Description,
                Url = dest.Image,
                HikingCategory = dest.HikingCategory,
                HikingTour = dest.HikingTour,
                TrailClass = dest.TrailClass,
                ItineraryId = (int)dest.ItineraryId,
                Itinerary = dest.Itinerary,

            };
            return View(destVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDestinationViewModel editDestViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to update Destination");
                return View("Edit",editDestViewModel);
            }

            var getDestination = await _destinationRepository.GetDestinationByIdAsyncNoTracking(id);
            if (getDestination != null)
            {
                try
                {
                    var file = new FileInfo(getDestination.Image);
                    var publicId = Path.GetFileNameWithoutExtension(file.Name);

                    await _photoService.DeletePhotoAsync(publicId);
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(editDestViewModel);
                }

                var imageResult = await _photoService.AddPhotoAsync(editDestViewModel.Image);

                var editDestination = new Destination
                {
                    Id = id,
                    Title = editDestViewModel.Title,
                    Description = editDestViewModel.Description,
                    Image = imageResult.SecureUrl.AbsoluteUri,
                    HikingCategory = editDestViewModel.HikingCategory,
                    HikingTour = editDestViewModel.HikingTour,
                    TrailClass = editDestViewModel.TrailClass,
                    ItineraryId = editDestViewModel.ItineraryId,
                    Itinerary = editDestViewModel.Itinerary
                };

                await _destinationRepository.Update(editDestination);

                return RedirectToAction("Index");
            }
            else
            {
                return View(editDestViewModel);
            }


        }

        public async Task<IActionResult> Delete(int id)
        {
            var dest = await _destinationRepository.GetDestinationByIdAsync(id);
            if (dest == null) return View("Error");

            return View(dest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var dest = await _destinationRepository.GetDestinationByIdAsync(id);
            if(dest == null) return View("Error");

            if (!string.IsNullOrEmpty(dest.Image))
            {
                await _photoService.DeletePhotoAsync(dest.Image);
            }

            await _destinationRepository.Delete(dest);
            return RedirectToAction("Index");
        }
    }
}
