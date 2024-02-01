using Core.DTO;
using Core.Entities;

namespace Core.Services;

public interface ILocationService
{
    Task<IEnumerable<LocationDto>> GetLocationsWithAvailability(TimeSpan openingTime, TimeSpan closingTime);
}