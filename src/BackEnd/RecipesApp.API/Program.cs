using Microsoft.AspNetCore.Mvc;
using RecipesApp.API.Converters;
using RecipesApp.API.Filters;
using RecipesApp.API.Middlewares;
using RecipesApp.Application;
using RecipesApp.Infra.Data.Migrations;
using RecipesApp.Infra.Extensions;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(
    opt => opt.Filters.Add(typeof(ExceptionFilter))
);

builder.Services.AddRouting(
    opt =>
    {
        opt.LowercaseUrls = true;
        opt.LowercaseQueryStrings = true;
    });

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDb(builder.Configuration);

app.Run();

void MigrateDb(IConfiguration configuration)
{
    if (configuration.IsTesting())
        return;

    var scope = app.Services
        .GetRequiredService<IServiceScopeFactory>().CreateScope();

    MigrationDb.Migrate(
        configuration.ConnectionString(),
        scope.ServiceProvider
    );
}

public abstract partial class Program;