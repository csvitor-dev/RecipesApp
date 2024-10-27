using RecipesApp.Domain.Repositories;

namespace RecipesApp.Infra.Data;

public class PersistenceUnitOfWork(RecipesAppContext context)
    : IUnitOfWork
{
    private readonly RecipesAppContext _context = context;

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();
}
