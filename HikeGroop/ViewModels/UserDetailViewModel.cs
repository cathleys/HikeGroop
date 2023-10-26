using HikeGroop.Data.Enums;
using HikeGroop.Models;

namespace HikeGroop.ViewModels;

public class UserDetailViewModel
{

    public string? Id { get; set; }
    public string? UserName { get; set; }
    public HikerType? HikerType { get; set; }
    public string? City { get; set; }
    public string? ProfileImageUrl { get; set; }
}
