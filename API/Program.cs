using Microsoft.EntityFrameworkCore;
using SearchLocationApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LocationContext>(options =>
{
    options.UseSqlite("Data Source=Locations.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/locations", async (LocationContext context) =>
{
    var locations = await context.Locations
        .Where(l => l.OpeningTime >= new TimeSpan(10, 0, 0) && l.ClosingTime <= new TimeSpan(13, 0, 0))
        .ToListAsync();

    return locations;
}).WithName("GetLocations").WithOpenApi();

app.Run();