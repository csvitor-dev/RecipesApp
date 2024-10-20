using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;

namespace RecipesApp.Application.UseCases.User.Register;

public class RegisterUserUC
{
    private readonly RegisterUserValidator _validator = new();

    public RegisterUserResponseJSON Execute(RegisterUserRequestJSON request)
    {
        Validate(request);

        return new(request.Name);
    }
    private void Validate(RegisterUserRequestJSON request)
    {
        var result = _validator.Validate(request);

        if (result.IsValid)
            return;
        var errorMessages = from errors in result.Errors select errors.ErrorMessage;

        throw new Exception();
    }
}
