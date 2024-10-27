using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Application.Services;
using RecipesApp.Application.UseCases.User.Register;

namespace RecipesApp.Application;

public static class ApplicationDIExtension
{
    public static void AddApplication(this IServiceCollection self, IConfiguration configuration)
    {
        var reference = configuration.GetSection("Settings:key");
        AddServices(self, reference.Value);
        AddUseCases(self);
    }

    private static void AddServices(IServiceCollection services, string? key)
    {
        services.AddScoped((service) =>
            new MapperConfiguration((opt) =>
            {
                opt.AddProfile(new AutoMappingService());
            }).CreateMapper()
        );
        services.AddScoped((service) =>
            new PasswordEncryptionService(key ?? string.Empty)
        );
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUC, RegisterUserUC>();
    }
}
