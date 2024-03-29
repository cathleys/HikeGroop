﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikeGroop.Models;

public class Group
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public int? AddressId { get; set; }
    public Address? Address { get; set; }

    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
