using Core.Entities;

namespace Core.DTO;

public class LocationDto
{
    public LocationDto(Guid id, string name, TimeSpan openingTime, TimeSpan closingTime)
    {
        Id = id;
        Name = name;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }

    public Guid Id { get; }
    public string Name { get; }
    public TimeSpan OpeningTime { get; }
    public TimeSpan ClosingTime { get; }

    public static LocationDto FromEntity(Location location)
    {
        return new LocationDto(id: location.Id, name: location.Name,
            openingTime: TimeSpan.FromSeconds(location.OpeningTimeInSeconds),
            closingTime: TimeSpan.FromSeconds(location.ClosingTimeInSeconds));
    }
    public Location ToEntity()
    {
        return new Location(Id, Name, (long)OpeningTime.TotalSeconds, (long)ClosingTime.TotalSeconds);
    }
}