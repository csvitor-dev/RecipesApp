using FluentMigrator;

namespace RecipesApp.Infra.Data.Migrations.Versions;

[Migration(DbVersions.UsersTable,
    "creating table to save user's information")]
public sealed class MigrationInitial : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();
    }
}