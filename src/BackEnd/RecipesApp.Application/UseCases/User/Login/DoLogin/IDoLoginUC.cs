using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;

namespace RecipesApp.Application.UseCases.User.Login.DoLogin;

public interface IDoLoginUC
{
    public Task<RegisterUserResponseJSON> Execute(LoginUserRequestJSON request);
}
