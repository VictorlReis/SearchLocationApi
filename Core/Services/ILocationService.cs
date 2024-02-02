using Core.DTO;
using Core.Entities;
using FluentValidation.Results;

namespace Core.Services;

public interface ILocationService
{
    Task<IEnumerable<LocationDto>> GetLocationsWithAvailability(TimeSpan openingTime, TimeSpan closingTime);
    Task<LocationDto> CreateLocation(CreateLocationDto createLocationDto);
    Task<ValidationResult> ValidateCreateLocationDto(CreateLocationDto createLocationDto);
}