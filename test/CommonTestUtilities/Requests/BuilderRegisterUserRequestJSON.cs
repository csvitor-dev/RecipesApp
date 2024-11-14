using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class BuilderRegisterUserRequestJSON
{
    public static RegisterUserRequestJSON Build()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f => new RegisterUserRequestJSON(
                f.Person.FirstName,
                f.Internet.Email(f.Person.FirstName),
                f.Internet.Password()
            )).Generate();
}