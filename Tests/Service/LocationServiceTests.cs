using Core.Entities;
using Core.Repository;
using Core.Services;
using Moq;

namespace Tests.Service;

public class LocationServiceTests
{
    [Fact]
    public async Task GetLocationsWithAvailability_ReturnsLocations_WhenAvailable()
    {
        var openingTime = TimeSpan.FromHours(10);
        var closingTime = TimeSpan.FromHours(13);

        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(repo =>
                repo.GetLocationsWithAvailability(It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync(new List<Location>
            {
                new(Guid.NewGuid(), "TestLocation", 36000, 46800), // Opening at 10:00 AM, Closing at 1:00 PM
            });

        var locationService = new LocationService(mockLocationRepository.Object);

        var result = await locationService.GetLocationsWithAvailability(openingTime, closingTime);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal("TestLocation", result.First().Name);
    }

    [Fact]
    public async Task GetLocationsWithAvailability_ReturnsEmptyList_WhenNotAvailable()
    {
        var openingTime = TimeSpan.FromHours(10);
        var closingTime = TimeSpan.FromHours(13);

        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(repo =>
                repo.GetLocationsWithAvailability(It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync(new List<Location>());

        var locationService = new LocationService(mockLocationRepository.Object);

        var result = await locationService.GetLocationsWithAvailability(openingTime, closingTime);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}