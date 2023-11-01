using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HikeGroop.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace HikeGroop.Models;

public class AppUser : IdentityUser
{
    public HikerType? HikerType { get; set; }

    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }

    public ICollection<Destination>? Destinations { get; set; }
}
