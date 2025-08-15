namespace RecipesApp.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task AddUserAsync(Entities.User user);
}
