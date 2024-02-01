using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace SearchLocationApi;

public class LocationContext : DbContext
{
    public LocationContext(DbContextOptions<LocationContext> options) : base(options)
    {
        // Ensure the database and table are created
        Database.EnsureCreated();
    }

    public DbSet<Location> Locations { get; set; }
}