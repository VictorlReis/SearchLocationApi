using Microsoft.EntityFrameworkCore;

namespace SearchLocationApi.Extensions;

public static class ApplicationServiceExtensions
{

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("locationSqlite");
        services.AddDbContext<LocationContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
    }
}