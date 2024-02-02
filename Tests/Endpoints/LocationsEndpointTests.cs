using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tests.Utils;

namespace Tests.Endpoints;

public class LocationsEndpointTests
{
    private readonly Mock<ILocationService> _locationService;
    public LocationsEndpointTests()
    {
        _locationService = new Mock<ILocationService>();
    }
    
    [Fact]
    public async Task GetLocations_ReturnsStatus200AndNonEmptyContent()
    {
        _locationService.Setup(x => x.GetLocationsWithAvailability(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
            .ReturnsAsync(new List<LocationDto>()
            {
                new LocationDto(Guid.NewGuid(), "Test new location", new TimeSpan(09, 00, 0), new TimeSpan(15, 0, 0))
            });
        await using var app = new LocationsEndpointsTestsApp(x =>
        {
            x.AddSingleton(_locationService.Object);
        });
        
        using var client = app.CreateClient();
        
        var response = await client.GetAsync("/locations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetLocations_ReturnsStatus404AndEmptyContent_WhenNoLocations()
     {
        _locationService.Setup(x => x.GetLocationsWithAvailability(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
            .ReturnsAsync(new List<LocationDto>());
        await using var app = new LocationsEndpointsTestsApp(x =>
        {
            x.AddSingleton(_locationService.Object);
        });
        
        using var client = app.CreateClient();
        
        var response = await client.GetAsync("/locations");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
     }
    
    
    
     [Fact]
    public async Task CreateLocation_ReturnsStatus201AndLocationDto_WhenValidDto()
    {
        var createLocationDto = new CreateLocationDto("New Location", "09:00", "17:00");
        _locationService.Setup(x => x.ValidateCreateLocationDto(createLocationDto))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _locationService.Setup(x => x.CreateLocation(createLocationDto))
            .ReturnsAsync(new LocationDto(Guid.NewGuid(), createLocationDto.Name, TimeSpan.Parse(createLocationDto.OpeningTime), TimeSpan.Parse(createLocationDto.ClosingTime)));

        await using var app = new LocationsEndpointsTestsApp(x =>
        {
            x.AddSingleton(_locationService.Object);
        });

        using var client = app.CreateClient();
        var response = await client.PostAsJsonAsync("/locations", createLocationDto);
        var locationDto = await response.Content.ReadFromJsonAsync<LocationDto>();
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(locationDto);
    }

    
    [Fact]
    public async Task CreateLocation_ReturnsStatus400_WhenInvalidDto()
    {
        var createLocationDto = new CreateLocationDto("A good name", "10:10", "12:00");
        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("OpeningTime", "Invalid opening time format. Please use 'hh:mm'.", "string"));
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("ClosingTime", "Invalid closing time format. Please use 'hh:mm'.", "string"));

        _locationService.Setup(x => x.ValidateCreateLocationDto(createLocationDto))
            .ReturnsAsync(validationResult);

        await using var app = new LocationsEndpointsTestsApp(x =>
        {
            x.AddSingleton(_locationService.Object);
        });

        using var client = app.CreateClient();

        var response = await client.PostAsJsonAsync("/locations", createLocationDto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var errors = await response.Content.ReadFromJsonAsync<List<ValidationError>>();

        Assert.NotNull(errors);
        Assert.Equal(2, errors.Count);

        Assert.Contains(errors, error => error.PropertyName == "OpeningTime" && error.ErrorMessage == "Invalid opening time format. Please use 'hh:mm'.");
        Assert.Contains(errors, error => error.PropertyName == "ClosingTime" && error.ErrorMessage == "Invalid closing time format. Please use 'hh:mm'.");
    }

}


