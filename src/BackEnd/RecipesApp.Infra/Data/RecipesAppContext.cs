using Microsoft.EntityFrameworkCore;
using RecipesApp.Domain.Entities;

namespace RecipesApp.Infra.Data;

public class RecipesAppContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipesAppContext).Assembly);
}
