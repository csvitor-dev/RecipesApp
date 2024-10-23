using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Application.Services;
using RecipesApp.Application.UseCases.User.Register;

namespace RecipesApp.Application;

public static class ApplicationDIExtension
{
    public static void AddApplication(this IServiceCollection self)
    {
        AddServices(self);
        AddUseCases(self);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped((service) =>
            new MapperConfiguration((opt) =>
            {
                opt.AddProfile(new AutoMappingService());
            }).CreateMapper()
        );
        services.AddScoped((service) =>
            new PasswordEncryptionService()
        );
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUC, RegisterUserUC>();
    }
}
