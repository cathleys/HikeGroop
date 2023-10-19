using HikeGroop.Data.Enums;
using HikeGroop.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikeGroop.ViewModels
{
    public class CreateDestinationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public HikingCategory HikingCategory { get; set; }
        public HikingTour HikingTour { get; set; }
        public TrailClass TrailClass { get; set; }


        public Itinerary? Itinerary { get; set; }
    }
}
