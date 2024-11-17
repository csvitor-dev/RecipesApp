using Bogus;
using RecipesApp.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class BuilderRegisterUserRequestJSON
{
    public static RegisterUserRequestJSON CreateMock()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f =>
            {
                var name = f.Person.FirstName;

                return new RegisterUserRequestJSON(
                    name,
                    f.Internet.Email(name),
                    f.Internet.Password()
                );
            }).Generate();

    public static RegisterUserRequestJSON CreateMockWithOutName()
        => new Faker<RegisterUserRequestJSON>()
            .CustomInstantiator(f => new RegisterUserRequestJSON(
                Email: f.Internet.Email(),
                Password: f.Internet.Password()
            )).Generate();
}