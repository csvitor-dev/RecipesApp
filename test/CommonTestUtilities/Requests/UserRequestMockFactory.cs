using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class UserRequestMockFactory
{
    public static LoginUserRequestJSON CreateLoginRequestMock(int length = 10, string? invalidEmail = null)
        => new Faker<LoginUserRequestJSON>()
            .CustomInstantiator(f =>  new LoginUserRequestJSON())
            .RuleFor(r => r.Email,
                f => invalidEmail ?? f.Internet.Email())
            .RuleFor(r => r.Password,
                f => f.Internet.Password(length))
            .Generate();
    
    public static RegisterUserRequestJSON CreateRegisterRequestMock(int length = 10, string? invalidEmail = null)
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f =>  new RegisterUserRequestJSON())
            .RuleFor(r => r.Name,
                f => f.Person.FirstName)
            .RuleFor(r => r.Email,
                (f, r) =>
                    invalidEmail ?? f.Internet.Email(r.Name))
            .RuleFor(r => r.Password,
                f => f.Internet.Password(length))
            .Generate();
}