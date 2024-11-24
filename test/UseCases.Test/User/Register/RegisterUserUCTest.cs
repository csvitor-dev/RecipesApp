using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services;
using FluentAssertions;
using RecipesApp.Application.UseCases.User.Register;

namespace UseCases.Test.User.Register;

public class RegisterUserUCTest
{
    private RegisterUserUC CreateUseCase()
    {
        var r = new UserReadOnlyRepositoryMockFactory().CreateMock();
        var w = UserWriteOnlyRepositoryMockFactory.CreateMock();
        var uw = UnitOfWorkMockFactory.CreateMock();
        var map = MapperMockFactory.CreateMock();
        var pw = EncryptMockFactory.CreateMock();
        
        return new RegisterUserUC(r, w, uw, map, pw);
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
}