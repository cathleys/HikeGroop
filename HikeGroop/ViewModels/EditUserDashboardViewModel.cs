using System.ComponentModel.DataAnnotations;
using HikeGroop.Data.Enums;

namespace HikeGroop.ViewModels;

public class EditUserDashboardViewModel
{
    public string Id { get; set; }
    [Display(Name = "Hiker Type")]
    public HikerType HikerType { get; set; }
    public string? ProfileImageUrl { get; set; }
    public IFormFile Image { get; set; }
    public string City { get; set; }
}
