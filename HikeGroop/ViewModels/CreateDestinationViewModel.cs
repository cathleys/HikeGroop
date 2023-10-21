using HikeGroop.Data.Enums;
using HikeGroop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikeGroop.ViewModels
{
    public class CreateDestinationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        [Display(Name = "HIking Category")]
        public HikingCategory HikingCategory { get; set; }
        [Display(Name = "Hiking Tour")]
        public HikingTour HikingTour { get; set; }
        [Display(Name = "Trail Class")]
        public TrailClass TrailClass { get; set; }


        public Itinerary? Itinerary { get; set; }
        public string AppUserId { get; set; }
    }
}
