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
        var request = RegisterUserRequestJSONMockFactory.CreateMock();
        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Test_OnFailureWith_EmptyName()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutName();
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.NAME_REQUIRED));
    }

    [Fact]
    public void Test_OnFailureWith_EmptyEmail()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutEmail();
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.EMAIL_REQUIRED));
    }
    [Fact]
    public void Test_OnFailureWith_InvalidEmail()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithInvalidEmail("email.com");
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.EMAIL_INVALID));
    }

    [Fact]
    public void Test_OnFailureWith_EmptyPassword()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMockWithoutPassword();
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.PASSWORD_REQUIRED));
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Test_OnFailureWith_InvalidPassword(int passwordLength)
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock(passwordLength);
        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourcesAccessor.PASSWORD_LENGTH));
    }
}