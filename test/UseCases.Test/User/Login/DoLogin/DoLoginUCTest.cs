using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using RecipesApp.Application.UseCases.User.Login.DoLogin;
using RecipesApp.Communication.Requests;
using RecipesApp.Exception.Project;
using RecipesApp.Exception.Resources;

namespace UseCases.Test.User.Login.DoLogin;

public class DoLoginUCTest
{
    private static DoLoginUC CreateUseCase(RecipesApp.Domain.Entities.User? user = null)
    {
        var factory = new UserReadOnlyRepositoryMockFactory();

        if (user is not null)
            factory.GetUserByEmail(user);
        var pw = EncryptMockFactory.CreateMock();

        return new DoLoginUC(factory.CreateMock(), pw);
    }

    [Fact]
    public async Task Test_OnSuccess()
    {
        var (user, originalPassword) = UserMockFactory.CreateMock();
        var uc = CreateUseCase(user);

        var result = await uc.Execute(new LoginUserRequestJSON(user.Email, originalPassword));

        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
    }

    [Fact]
    public async Task Test_InvalidUser_OnFailure()
    {
        var request = UserRequestMockFactory.CreateLoginRequestMock();
        var uc = CreateUseCase();

        var exception = await Assert.ThrowsAsync<InvalidLoginException>(Act);
        Assert.True(exception.Payload.Count == 1);
        Assert.True(exception.Payload
                .Contains(ResourcesAccessor.EMAIL_OR_PASSWORD_INVALID));
        return;

        async Task Act() => await uc.Execute(request);
    }
}