using HikeGroop.Models;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
