using Core.Services;
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

app.MapGet("/locations", async (ILocationService locationService) => 
        await locationService.GetLocationsWithAvailability(new TimeSpan(10, 0, 0), new TimeSpan(13, 0, 0))).
    WithName("GetLocations").WithOpenApi();

app.Run();