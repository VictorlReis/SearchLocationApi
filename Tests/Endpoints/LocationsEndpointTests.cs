using System.Net;
using System.Text.Json;
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
    public async Task GetLocations_ReturnsStatus200AndNonEmptyContent()
    {
        await using var application = new PlaygroundApplication();
        using var client = application.CreateClient();
        
        var response = await client.GetAsync("/locations");
        var contentString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(contentString);
    }
    
    [Fact]
    public async Task GetLocations_ReturnsStatus200AndEmptyContent_WhenNoLocations()
    {
        await using var application = new PlaygroundApplication();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/locations");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var contentString = await response.Content.ReadAsStringAsync();
        var locations = JsonSerializer.Deserialize<List<LocationDto>>(contentString);

        Assert.NotNull(locations);
        Assert.Empty(locations);
    }
}