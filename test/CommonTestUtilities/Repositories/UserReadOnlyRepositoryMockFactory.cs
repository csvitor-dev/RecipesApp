using Moq;
using RecipesApp.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryMockFactory
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();

    public void ExistsActiveUserWithEmail(string email)
        => _repository.Setup(repo
            => repo.ExistsActiveUserWithEmailAsync(email)
        ).ReturnsAsync(true);

    public void GetUserByEmail(RecipesApp.Domain.Entities.User user)
        => _repository.Setup(repo
            => repo.GetByEmailAsync(user.Email)
        ).ReturnsAsync(user);

    public IUserReadOnlyRepository CreateMock()
        => _repository.Object;
}