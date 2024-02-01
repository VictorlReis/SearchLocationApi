using Core.Entities;

namespace Core.DTO;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

    // Additional properties...

    public static LocationDto FromEntity(Location location)
    {
        return new LocationDto
        {
            Id = location.Id,
            Name = location.Name,
            OpeningTime = TimeSpan.FromSeconds(location.OpeningTimeInSeconds),
            ClosingTime = TimeSpan.FromSeconds(location.ClosingTimeInSeconds),
        };
    }
}