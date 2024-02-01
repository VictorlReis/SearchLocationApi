using Core.Entities;

namespace Core.Repository;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetLocationsWithAvailability(long openingTimeInSeconds, long closingTimeInSeconds);
}