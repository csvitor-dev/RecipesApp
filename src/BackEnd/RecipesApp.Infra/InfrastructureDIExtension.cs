using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Domain.Repositories;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Infra.Data;
using RecipesApp.Infra.Data.Repositories;

namespace RecipesApp.Infra;

public static class InfrastructureDIExtension
{
    public static void AddInfra(this IServiceCollection self, IConfiguration configuration)
    {
        AddDbContext(self, 
            configuration.GetConnectionString("db"));
        AddRepositories(self);
    }

    private static void AddDbContext(IServiceCollection services, string? connection)
    {
        services.AddDbContext<RecipesAppContext>(
            (opt) =>
            {
                opt.UseSqlServer(connection); // potential exception
            }
        );
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, PersistenceUnitOfWork>();
    }
}
