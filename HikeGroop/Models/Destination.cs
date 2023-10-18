using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HikeGroop.Data.Enums;

namespace HikeGroop.Models;

public class Destination
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public HikingCategory HikingCategory { get; set; }
    public HikingTour HikingTourType { get; set; }
    public TrailClass TrailClass { get; set; }

    [ForeignKey("Itinerary")]
    public int? ItineraryId { get; set; }
    public Itinerary? Itinerary { get; set; }


    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; } // users who've been in the destination
}
