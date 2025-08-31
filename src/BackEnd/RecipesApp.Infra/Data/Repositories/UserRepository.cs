using Microsoft.EntityFrameworkCore;
using RecipesApp.Domain.Entities;
using RecipesApp.Domain.Repositories.User;

namespace RecipesApp.Infra.Data.Repositories;

public class UserRepository(RecipesAppContext context)
    : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly RecipesAppContext _context = context;

    public async Task AddUserAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithEmailAsync(string email)
    {
        var result = _context.Users
            .AnyAsync(u => u.Active && u.Email.Equals(email));

        return await result;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Active && u.Email.Equals(email));
}