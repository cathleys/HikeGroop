using HikeGroop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Data;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
