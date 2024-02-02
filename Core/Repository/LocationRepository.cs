using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public class LocationRepository : ILocationRepository
{
    private readonly LocationContext _context;

    public LocationRepository(LocationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Location>> GetLocationsWithAvailability(long openingTimeInSeconds, long closingTimeInSeconds) => 
        await _context.Locations.Where(l => l.OpeningTimeInSeconds <= closingTimeInSeconds 
                                               && l.ClosingTimeInSeconds >= openingTimeInSeconds).ToListAsync();
} 