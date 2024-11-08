using Microsoft.Extensions.Configuration;

namespace RecipesApp.Infra.Extensions;

public static class InfrastructureConfigurationExtension
{
    public static string ConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("db")!;
}
