using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace RecipesApp.Infra.Data.Migrations;

public static class MigrationDb
{
    public static void Migrate(string connection, IServiceProvider serviceProvider)
    {
        SqlConnectionStringBuilder builder = new(connection);
        var name = builder.InitialCatalog;
        builder.Remove("Database");

        EnsureDatabaseCreated(builder.ConnectionString, name);
        RunService(serviceProvider);
    }
    
    private static void EnsureDatabaseCreated(string connection, string dbName)
    {
        using SqlConnection db = new(connection);

        DynamicParameters parameters = new();
        parameters.Add("name", dbName);

        var records = db.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (records.Any() == false)
            db.Execute($"CREATE DATABASE {dbName}");
    }

    private static void RunService(IServiceProvider provider)
    {
        var runner = provider.GetRequiredService<IMigrationRunner>();
        
        runner.ListMigrations();
        runner.MigrateUp();
    }
}
