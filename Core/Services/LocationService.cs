using Core.DTO;
using Core.Repository;

namespace Core.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<IEnumerable<LocationDto>> GetLocationsWithAvailability(TimeSpan openingTime, TimeSpan closingTime)
    {
        var openingTimeInSeconds = (long)openingTime.TotalSeconds;
        var closingTimeInSeconds = (long)closingTime.TotalSeconds;

        var locations = await _locationRepository.GetLocationsWithAvailability(openingTimeInSeconds, closingTimeInSeconds);

        var locationDtos = locations.Select(LocationDto.FromEntity);

        return locationDtos;
    }
}