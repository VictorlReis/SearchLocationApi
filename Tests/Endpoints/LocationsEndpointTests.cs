using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Tests.Endpoints;

public class LocationsEndpointTests
{
    [Fact]
    public async Task GetLocations_ReturnsOkResult_WhenLocationsAvailable()
    {
        var mockLocationService = new Mock<ILocationService>();
        mockLocationService.Setup(service =>
                service.GetLocationsWithAvailability(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
            .ReturnsAsync(new List<LocationDto>(new List<LocationDto>() {new()}));

        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/locations");

        response.EnsureSuccessStatusCode();
    }
}