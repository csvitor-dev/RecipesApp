using CommonTestUtilities.Requests;
using RecipesApp.Application.UseCases.User.Register;
using RecipesApp.Exception.Resources;

namespace Validators.Test.User;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Test_OnSuccess()
    {
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock();

        var result = v.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Test_OnFailureWith_EmptyName()
    {
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock();
        request.Name = string.Empty;

        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.NAME_REQUIRED));
    }

    [Fact]
    public void Test_OnFailureWith_EmptyEmail()
    {
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock();
        request.Email = string.Empty;

        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.EMAIL_REQUIRED));
    }

    [Fact]
    public void Test_OnFailureWith_InvalidEmail()
    {
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock(invalidEmail: "email.com");

        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.EMAIL_INVALID));
    }

    [Fact]
    public void Test_OnFailureWith_EmptyPassword()
    {
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock();
        request.Password = string.Empty;

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
        var v = new RegisterUserValidator();
        var request = UserRequestMockFactory.CreateRegisterRequestMock(passwordLength);

        var result = v.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors,
            error => error.ErrorMessage.Equals(ResourcesAccessor.PASSWORD_LENGTH));
    }
}