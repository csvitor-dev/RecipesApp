using Bogus;
using CommonTestUtilities.Services;
using RecipesApp.Communication.Requests;
using RecipesApp.Domain.Entities;

namespace CommonTestUtilities.Entities;

public static class UserMockFactory
{
    private static int Id = 1;

    public static (User user, string password) CreateMock()
    {
        var encripter = EncryptMockFactory.CreateMock();
        var originalPassword = new Faker().Internet.Password();

        var mock = new Faker<User>()
            .RuleFor(u => u.ID, () => Id++)
            .RuleFor(u => u.Name, f => f.Person.FirstName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, encripter.Encrypt(originalPassword))
            .Generate();

        return (mock, originalPassword);
    }
}