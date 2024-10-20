using AutoMapper;
using RecipesApp.Communication.Requests;
using RecipesApp.Domain.Entities;

namespace RecipesApp.Application.Services;

internal class AutoMappingService : Profile
{
    internal AutoMappingService()
    {
        RequestToDomain();
    }

    private void RequestToDomain() => 
        CreateMap<RegisterUserRequestJSON, User>()
            .ForMember((dest) => dest.Password, (opt) => opt.Ignore());
}
