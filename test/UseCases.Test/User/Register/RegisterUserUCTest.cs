using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using RecipesApp.Application.UseCases.User.Register;
using RecipesApp.Exception.Project;
using RecipesApp.Exception.Resources;

namespace UseCases.Test.User.Register;

public class RegisterUserUCTest
{
    private RegisterUserUC CreateUseCase(string? email = null)
    {
        var factory = new UserReadOnlyRepositoryMockFactory();

        if (string.IsNullOrEmpty(email) is false)
            factory.ExistActiveUserWithEmail(email);
        var w = UserWriteOnlyRepositoryMockFactory.CreateMock();
        var uw = UnitOfWorkMockFactory.CreateMock();
        var pw = EncryptMockFactory.CreateMock();

        return new RegisterUserUC(factory.CreateMock(), w, uw, pw);
    }

    [Fact]
    public async Task Test_OnSuccess()
    {
        var request = UserRequestMockFactory.CreateRegisterRequestMock();
        var uc = CreateUseCase();

        var result = await uc.Execute(request);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
    }

    [Fact]
    public async Task Test_OnFailure_WithEmailAlreadyRegistered()
    {
        var request = UserRequestMockFactory.CreateRegisterRequestMock();
        var uc = CreateUseCase(request.Email);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(Act);
        Assert.True(exception.ErrorMessages.Count == 1);
        Assert.True(exception.ErrorMessages
                .Contains(ResourcesAccessor.EMAIL_ALREADY_REGISTERED));
        return;

        async Task Act() => await uc.Execute(request);
    }
}