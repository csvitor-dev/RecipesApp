using CommonTestUtilities.Requests;
using FluentAssertions;
using RecipesApp.Application.UseCases.User.Register;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Test_OnSuccess()
    {
        RegisterUserValidator v = new();
        var request = BuilderRegisterUserRequestJSON.Build();
        var result = v.Validate(request);

        result.IsValid.Should().BeTrue();
    }
}