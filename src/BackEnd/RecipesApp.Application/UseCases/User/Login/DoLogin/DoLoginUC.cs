using RecipesApp.Application.Services;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Exception.Project;

namespace RecipesApp.Application.UseCases.User.Login.DoLogin;

public class DoLoginUC(
    IUserReadOnlyRepository readRepo,
    PasswordEncryptionService service
) : IDoLoginUC
{
    public async Task<RegisterUserResponseJSON> Execute(LoginUserRequestJSON request)
    {
        await ValidateAsync(request);

        var user = await readRepo.GetByEmailAsync(request.Email);
        var passwordsNoMatch = service.Encrypt(request.Password) != user?.Password;

        if (user is null || passwordsNoMatch)
            throw new InvalidLoginException();

        return new RegisterUserResponseJSON(user.Name);
    }

    private static async Task ValidateAsync(LoginUserRequestJSON request)
    {
        var result = await new DoLoginValidator().ValidateAsync(request);

        if (result.IsValid)
            return;
        var errorMessages = (from errors in result.Errors select errors.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}