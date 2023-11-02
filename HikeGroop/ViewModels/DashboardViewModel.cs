using HikeGroop.Models;

namespace HikeGroop.ViewModels;

public class DashboardViewModel
{
    public List<Group>? Groups { get; set; }
    public string AppUserId { get; set; }
    public List<Destination>? Destinations { get; set; }
}
