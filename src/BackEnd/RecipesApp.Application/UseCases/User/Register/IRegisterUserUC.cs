using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;

namespace RecipesApp.Application.UseCases.User.Register;

public interface IRegisterUserUC
{
    public Task<RegisterUserResponseJSON> Execute(RegisterUserRequestJSON request);
}
