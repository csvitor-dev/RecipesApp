using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using FluentAssertions;
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
        var map = MapperMockFactory.CreateMock();
        var pw = EncryptMockFactory.CreateMock();

        return new RegisterUserUC(factory.CreateMock(), w, uw, map, pw);
    }

    [Fact]
    public async Task Test_OnSuccess()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock();
        var uc = CreateUseCase();

        var result = await uc.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Test_OnFailure_WithEmailAlreadyRegistered()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock();
        var uc = CreateUseCase(request.Email);

        Func<Task> act = async () => await uc.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1
                        && e.ErrorMessages.Contains(ResourcesAccessor.EMAIL_ALREADY_REGISTERED));
    }
}