using Moq;
using RecipesApp.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryMockFactory
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();
    
    public IUserReadOnlyRepository CreateMock()
        => _repository.Object;
}