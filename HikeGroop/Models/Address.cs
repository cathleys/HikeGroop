using System.ComponentModel.DataAnnotations;

namespace HikeGroop.Models;

public class Address
{
    [Key]
    public int Id { get; set; }
    public string? City { get; set; }
}
