using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Infra.Data;
using RecipesApp.Infra.Data.Repositories;

namespace RecipesApp.Infra;

public static class InfrastructureDIExtension
{
    public static void AddInfra(this IServiceCollection self)
    {
        AddDbContext(self);
        AddRepositories(self);
    }

    private static void AddDbContext(IServiceCollection services)
    {
        services.AddDbContext<RecipesAppContext>(
            (opt) =>
            {
                opt.UseSqlServer("");
            }
        );
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
}
