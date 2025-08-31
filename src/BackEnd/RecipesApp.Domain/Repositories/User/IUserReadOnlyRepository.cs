namespace RecipesApp.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmailAsync(string email);
    public Task<Entities.User?> GetByEmailAsync(string email);
}
