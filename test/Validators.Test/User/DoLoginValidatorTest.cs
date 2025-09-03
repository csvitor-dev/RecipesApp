using CommonTestUtilities.Requests;
using RecipesApp.Application.UseCases.User.Login.DoLogin;
using RecipesApp.Exception.Resources;

namespace Validators.Test.User;

public class DoLoginValidatorTest
{
    [Fact]
    public void Test_OnSuccess()
    {
        var v = new DoLoginValidator();
        var request = LoginUserRequestJSONMockFactory.CreateMock();

        var result = v.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Test_OnFailureWith_EmptyEmail()
    {
        var v = new DoLoginValidator();
        var request = LoginUserRequestJSONMockFactory.CreateMockWithoutEmail();
        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.EMAIL_REQUIRED));
    }
    
    [Fact]
    public void Test_OnFailureWith_InvalidEmail()
    {
        var v = new DoLoginValidator();
        var request = LoginUserRequestJSONMockFactory.CreateMockWithInvalidEmail();
        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.EMAIL_INVALID));
    }
    
    [Fact]
    public void Test_OnFailureWith_EmptyPassword()
    {
        var v = new DoLoginValidator();
        var request = LoginUserRequestJSONMockFactory.CreateMockWithoutPassword();
        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.PASSWORD_REQUIRED));
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Test_OnFailureWith_InvalidPassword(int passwordLength)
    {
        var v = new DoLoginValidator();
        var request = LoginUserRequestJSONMockFactory.CreateMock(passwordLength);
        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.PASSWORD_LENGTH));
    }
}