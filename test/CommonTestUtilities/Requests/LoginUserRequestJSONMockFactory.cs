using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class LoginUserRequestJSONMockFactory
{
    public static LoginUserRequestJSON CreateMock(int length = 10)
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f => new LoginUserRequestJSON(
                Email: f.Internet.Email(),
                Password: f.Internet.Password(length)
            )).Generate();

    public static LoginUserRequestJSON CreateMockWithInvalidEmail(int length = 10)
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f => new LoginUserRequestJSON(
                Email: f.Internet.Email(provider: string.Empty),
                Password: f.Internet.Password(length)
            )).Generate();

    public static LoginUserRequestJSON CreateMockWithoutEmail(int length = 10)
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f => new LoginUserRequestJSON(
                Password: f.Internet.Password(length)
            )).Generate();

    public static LoginUserRequestJSON CreateMockWithoutPassword()
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f => new LoginUserRequestJSON(
                Email: f.Internet.Email()
            )).Generate();
}