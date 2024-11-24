using Moq;
using RecipesApp.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryMockFactory
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();

    public void ExistActiveUserWithEmail(string email)
        => _repository.Setup(repo
            => repo.ExistsActiveUserWithEmailAsync(email)
        ).ReturnsAsync(true);

    public IUserReadOnlyRepository CreateMock()
        => _repository.Object;
}