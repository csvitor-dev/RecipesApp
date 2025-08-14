using Microsoft.Extensions.Configuration;

namespace RecipesApp.Infra.Extensions;

public static class InfrastructureConfigurationExtension
{
    public static string ConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("db")!;

    public static bool IsTesting(this IConfiguration configuration)
    {
        var sectionValue = configuration["EnvironmentTesting"];
        
        return sectionValue?.ToLower().Equals("true") ?? false;
    }
}
