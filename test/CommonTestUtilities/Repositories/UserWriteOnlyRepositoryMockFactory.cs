using Moq;
using RecipesApp.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class UserWriteOnlyRepositoryMockFactory
{
    public static IUserWriteOnlyRepository CreateMock()
        => new Mock<IUserWriteOnlyRepository>().Object;
}