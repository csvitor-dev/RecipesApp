using Dapper;
using Microsoft.Data.SqlClient;

namespace RecipesApp.Infra.Data.Migrations;

public static class MigrationDB
{
    public static void Migrate(string connection)
    {
        SqlConnectionStringBuilder builder = new(connection);
        string name = builder.InitialCatalog;
        builder.Remove("Database");

        EnsureDatabaseCreated(builder.ConnectionString, name);
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
}
