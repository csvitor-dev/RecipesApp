using AutoMapper;
using RecipesApp.Application.Services;

namespace CommonTestUtilities.Services;

public static class MapperMockFactory
{
    public static IMapper CreateMock()
    {
        return new MapperConfiguration(
            options => { options.AddProfile(new AutoMappingService()); }
        ).CreateMapper();
    }
}