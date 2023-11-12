using HikeGroop.Extensions;
using HikeGroop.Helpers;
using HikeGroop.Interfaces;
using HikeGroop.Models;
using HikeGroop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HikeGroop.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DestinationController(IUnitOfWork uow,
            IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Index(PaginationParams paginationParams,
        string searchString)
        {
            var dests = await _uow.DestinationRepository
            .GetDestinationsPerPage(paginationParams, searchString);

            return View(dests);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var dest = await _uow.DestinationRepository.GetDestinationByIdAsync(id);
            return View(dest);
        }

        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createDestinationViewModel = new CreateDestinationViewModel
            {
                AppUserId = currentUserId
            };
            return View(createDestinationViewModel);
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
                    AppUserId = editDestViewModel.AppUserId,
                    Itinerary = new Itinerary
                    {
                        MeetUp = editDestViewModel.Itinerary.MeetUp,
                        JumpOffPoint = editDestViewModel.Itinerary.JumpOffPoint,
                        Location = editDestViewModel.Itinerary.Location,
                        Elevation = editDestViewModel.Itinerary.Elevation,
                        DaysRequired = editDestViewModel.Itinerary.DaysRequired,

                    }

                };

                await _uow.DestinationRepository.Add(newDestination);
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

            var dest = await _uow.DestinationRepository.GetDestinationByIdAsync(id);

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
                AppUserId = dest.AppUserId

            };
            return View(destVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDestinationViewModel editDestViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to update Destination");
                return View("Edit", editDestViewModel);
            }

            var getDestination = await _uow.DestinationRepository.GetDestinationByIdAsyncNoTracking(id);
            if (getDestination != null)
            {
                try
                {
                    var file = new FileInfo(getDestination.Image);
                    var publicId = Path.GetFileNameWithoutExtension(file.Name);

                    await _photoService.DeletePhotoAsync(publicId);
                }
                catch (Exception)
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
                    Itinerary = editDestViewModel.Itinerary,
                    AppUserId = editDestViewModel.AppUserId

                };

                await _uow.DestinationRepository.Update(editDestination);

                return RedirectToAction("Index");
            }
            else
            {
                return View(editDestViewModel);
            }


        }

        public async Task<IActionResult> Delete(int id)
        {
            var dest = await _uow.DestinationRepository.GetDestinationByIdAsync(id);
            if (dest == null) return View("Error");

            return View(dest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var dest = await _uow.DestinationRepository.GetDestinationByIdAsync(id);
            if (dest == null) return View("Error");

            if (!string.IsNullOrEmpty(dest.Image))
            {
                await _photoService.DeletePhotoAsync(dest.Image);
            }

            await _uow.DestinationRepository.Delete(dest);
            return RedirectToAction("Index");
        }
    }
}
