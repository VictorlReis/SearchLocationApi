using System.Runtime.CompilerServices;
using Core.Services;
using SearchLocationApi.Extensions;

[assembly: InternalsVisibleTo("Tests")]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/locations", async (ILocationService locationService) =>
{
    var locations = await locationService
        .GetLocationsWithAvailability(new TimeSpan(10, 0, 0), new TimeSpan(13, 0, 0));

    var locationDtos = locations.ToList();
    return locationDtos.Any() ? Results.Ok(locationDtos) :
        Results.NotFound("There is no location available for this range");
}).WithName("GetLocations").WithOpenApi();

app.Run();
public partial class Program { }
