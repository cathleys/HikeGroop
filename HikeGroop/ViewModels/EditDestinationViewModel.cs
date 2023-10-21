using System.ComponentModel.DataAnnotations;
using HikeGroop.Data.Enums;
using HikeGroop.Models;

namespace HikeGroop.ViewModels
{
    public class EditDestinationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public IFormFile Image { get; set; }

        [Display(Name = "Hiking Category")]
        public HikingCategory HikingCategory { get; set; }
        [Display(Name = "Hiking Tour")]
        public HikingTour HikingTour { get; set; }
        [Display(Name = "Trail Class")]
        public TrailClass TrailClass { get; set; }


        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }
    }
}
