using Microsoft.EntityFrameworkCore;
using SearchLocationApi;
using SearchLocationApi.Extensions;

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

app.MapGet("/locations", async (LocationContext context) =>
{
    var locations = await context.Locations
        .Where(l => l.OpeningTime >= new TimeSpan(10, 0, 0) && l.ClosingTime <= new TimeSpan(13, 0, 0))
        .ToListAsync();

    return locations;
}).WithName("GetLocations").WithOpenApi();

app.Run();