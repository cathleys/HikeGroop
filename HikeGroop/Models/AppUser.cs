using System.ComponentModel.DataAnnotations;
using HikeGroop.Data.Enums;

namespace HikeGroop.Models;

public class AppUser
{
    [Key]
    public string? Id { get; set; }
    public HikerType? HikerType { get; set; }
    public Address? Address { get; set; }

    public ICollection<Destination>? Destinations { get; set; }
}
