using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests
{
    internal class PlaygroundApplication : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public PlaygroundApplication(string environment = "Development")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<LocationContext>(options =>
                    {
                        options.UseInMemoryDatabase("Tests");
                    });
                });

            return base.CreateHost(builder);
        }
    }
}