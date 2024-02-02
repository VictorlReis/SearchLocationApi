using System.Net;
using System.Text.Json;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

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
}