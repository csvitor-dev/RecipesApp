using Moq;
using RecipesApp.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class UnitOfWorkMockFactory
{
    public static IUnitOfWork CreateMock()
        => new Mock<IUnitOfWork>().Object;
}