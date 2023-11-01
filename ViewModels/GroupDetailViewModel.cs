using HikeGroop.Models;

namespace HikeGroop.ViewModels;

public class GroupDetailViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public int? AddressId { get; set; }
    public Address? Address { get; set; }

    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
    public AppUser? AppUser { get; set; }
}
