using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using FluentAssertions;
using RecipesApp.Application.UseCases.User.Register;

namespace UseCases.Test.User.Register;

public class RegisterUserUCTest
{
    [Fact]
    public async Task Test_OnSuccess()
    {
        var request = RegisterUserRequestJSONMockFactory.CreateMock();
        var uc = new RegisterUserUC();

        var result = await uc.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }
}