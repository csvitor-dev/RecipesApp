using Moq;
using RecipesApp.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryMockFactory
{
    public static IUserReadOnlyRepository CreateMock()
        => new Mock<IUserReadOnlyRepository>().Object;
}