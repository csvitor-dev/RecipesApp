using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class RegisterUserRequestJSONMockFactory
{
    public static RegisterUserRequestJSON CreateMock(int length = 10)
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f =>
            {
                var name = f.Person.FirstName;

                return new RegisterUserRequestJSON(
                    name,
                    f.Internet.Email(name),
                    f.Internet.Password(length)
                );
            }).Generate();

    public static RegisterUserRequestJSON CreateMockWithoutName()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f => new RegisterUserRequestJSON(
                Email: f.Internet.Email(),
                Password: f.Internet.Password()
            )).Generate();

    public static RegisterUserRequestJSON CreateMockWithoutEmail()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f => new RegisterUserRequestJSON(
                Name: f.Person.FirstName,
                Password: f.Internet.Password()
            )).Generate();

    public static RegisterUserRequestJSON CreateMockWithInvalidEmail(string? email = null)
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f => new RegisterUserRequestJSON(
                Name: f.Person.FirstName,
                Email: email ?? f.Internet.ExampleEmail(),
                Password: f.Internet.Password()
            )).Generate();

    public static RegisterUserRequestJSON CreateMockWithoutPassword()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f =>
            {
                var name = f.Person.FirstName;

                return new RegisterUserRequestJSON(
                    Name: name,
                    Email: f.Internet.Email(name)
                );
            }).Generate();
}