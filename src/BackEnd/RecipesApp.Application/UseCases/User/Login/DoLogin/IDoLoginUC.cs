using RecipesApp.Communication.Requests;

namespace RecipesApp.Application.UseCases.User.Login.DoLogin;

public interface IDoLoginUC
{
    public Task<RegisterUserRequestJSON> Execute(LoginUserRequestJSON request);
}
