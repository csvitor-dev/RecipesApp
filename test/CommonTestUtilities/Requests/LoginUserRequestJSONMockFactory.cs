using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class LoginUserRequestJSONMockFactory
{
    public static LoginUserRequestJSON CreateMock(int length = 10, string? invalidEmail = null)
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f =>  new LoginUserRequestJSON())
            .RuleFor(r => r.Email,
                f => invalidEmail ?? f.Internet.Email())
            .RuleFor(r => r.Password,
                f => f.Internet.Password(length))
            .Generate();
}