using Mapster;
using RecipesApp.Communication.Requests;
using RecipesApp.Domain.Entities;

namespace RecipesApp.Application.Services;

public static class MappingService
{
    public static void Configure()
    {
        TypeAdapterConfig<RegisterUserRequestJSON, User>
            .NewConfig()
            .Ignore(dest => dest.Password);
    }
}