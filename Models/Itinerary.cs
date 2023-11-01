using System.ComponentModel.DataAnnotations;

namespace HikeGroop.Models;

public class Itinerary
{
    [Key]
    public int Id { get; set; }
    public string? MeetUp { get; set; }
    public string? JumpOffPoint { get; set; }
    public string? Location { get; set; }
    public string? Elevation { get; set; }
    public int DaysRequired { get; set; }
    public int HoursToSummit { get; set; }

    public ICollection<Destination>? Destinations { get; set; }
}
