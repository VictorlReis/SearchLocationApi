using System.Net;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc.Testing;
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
    public async Task GetLocations_ReturnsOkResult_WhenLocationsAvailable()
    {
        _locationService.Setup(service =>
                service.GetLocationsWithAvailability(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
            .ReturnsAsync(new List<LocationDto>(new List<LocationDto>()
            {
                new (Guid.NewGuid(), "BarberShop Two", TimeSpan.FromHours(9), TimeSpan.FromHours(17)),
            }));

        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/locations");

        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GetLocations_ReturnsNoContent_WhenLocationsIsEmpty()
    {
        var mockLocationService = new Mock<ILocationService>();
        mockLocationService.Setup(service =>
                service.GetLocationsWithAvailability(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
            .ReturnsAsync(new List<LocationDto>());

        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/locations");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}