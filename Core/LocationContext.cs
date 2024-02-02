using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core;

public sealed class LocationContext : DbContext
{
    public LocationContext(DbContextOptions<LocationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Location> Locations { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var locations = new List<Location>
        {
            new (Guid.NewGuid(), "BarberShop Two", (int)TimeSpan.FromHours(9).TotalSeconds, (int)TimeSpan.FromHours(17).TotalSeconds),
            new (Guid.NewGuid(), "Bakery Fresh Bites", (int)TimeSpan.FromHours(6).TotalSeconds, (int)TimeSpan.FromHours(16).TotalSeconds),
            new (Guid.NewGuid(), "Bookstore Novel Corner", (int)TimeSpan.FromHours(10).TotalSeconds, (int)TimeSpan.FromHours(18).TotalSeconds),
            new (Guid.NewGuid(), "Candy Store Sweet Delight", (int)TimeSpan.FromHours(10).TotalSeconds, (int)TimeSpan.FromHours(19).TotalSeconds),
            new (Guid.NewGuid(), "Coffee Shop Brew Haven", (int)TimeSpan.FromHours(8).TotalSeconds, (int)TimeSpan.FromHours(20).TotalSeconds),
            new (Guid.NewGuid(), "Cinema Complex Movie Haven", (int)TimeSpan.FromHours(12).TotalSeconds, (int)TimeSpan.FromHours(23).TotalSeconds),
            new (Guid.NewGuid(), "Electronics Store Tech Hub", (int)TimeSpan.FromHours(9).TotalSeconds, (int)TimeSpan.FromHours(17).TotalSeconds),
            new (Guid.NewGuid(), "Pharmacy True Health", (int)TimeSpan.FromHours(7).TotalSeconds, (int)TimeSpan.FromHours(23).TotalSeconds),
            new (Guid.NewGuid(), "Supermarket Every Food", (int)TimeSpan.FromHours(7).TotalSeconds, (int)TimeSpan.FromHours(20).TotalSeconds),
            new (Guid.NewGuid(), "Fitness Center Active Life", (int)TimeSpan.FromHours(7).TotalSeconds, (int)TimeSpan.FromHours(21).TotalSeconds),
            new (Guid.NewGuid(), "Fitness Center Active Night", (int)TimeSpan.FromHours(17).TotalSeconds, (int)TimeSpan.FromHours(6).TotalSeconds),
        };
        modelBuilder.Entity<Location>().HasData(locations);
    }
}
