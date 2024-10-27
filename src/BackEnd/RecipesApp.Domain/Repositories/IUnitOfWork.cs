namespace RecipesApp.Domain.Repositories;

public interface IUnitOfWork
{
    public Task CommitAsync();
}
