using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core;

public sealed class LocationContext : DbContext
{
    public LocationContext(DbContextOptions<LocationContext> options) : base(options)
    {
        Database.EnsureCreated();
        Seed.Initialize(this);
    }


    public DbSet<Location> Locations { get; set; }
}
