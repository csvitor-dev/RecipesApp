using CommonTestUtilities.Requests;
using FluentAssertions;
using RecipesApp.Application.UseCases.User.Register;
using RecipesApp.Exception.Resources;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest : IDisposable
{
    private readonly RegisterUserValidator _validator = new();

    public void Dispose()
        => GC.SuppressFinalize(this);
    
    [Fact]
    public void Test_OnSuccess()
    {
        var request = BuilderRegisterUserRequestJSON.CreateMock();
        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Test_OnFailureWithEmptyName()
    {
        var request = BuilderRegisterUserRequestJSON.CreateMockWithOutName();
        
        var result = _validator.Validate(request!);
        
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.NAME_REQUIRED));
    }
}