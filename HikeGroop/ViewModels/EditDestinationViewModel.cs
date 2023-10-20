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

        public HikingCategory HikingCategory { get; set; }
        public HikingTour HikingTour { get; set; }
        public TrailClass TrailClass { get; set; }

    
        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }
    }
}
