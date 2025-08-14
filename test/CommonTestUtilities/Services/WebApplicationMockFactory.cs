using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Infra.Data;

namespace CommonTestUtilities.Services;

public class WebApplicationMockFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RecipesAppContext>));
                
                if (descriptor is not null)
                    services.Remove(descriptor);
                var provider = services.AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<RecipesAppContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDatabaseForTesting");
                    options.UseInternalServiceProvider(provider);
                });
            });
    }
}