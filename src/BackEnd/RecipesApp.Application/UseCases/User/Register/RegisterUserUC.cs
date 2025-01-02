using AutoMapper;
using RecipesApp.Application.Services;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;
using RecipesApp.Domain.Repositories;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Exception.Project;
using RecipesApp.Exception.Resources;

namespace RecipesApp.Application.UseCases.User.Register;

public class RegisterUserUC(
    IUserReadOnlyRepository readRepo,
    IUserWriteOnlyRepository writeRepo,
    IUnitOfWork uw,
    IMapper map,
    PasswordEncryptionService service
) : IRegisterUserUC
{
    public async Task<RegisterUserResponseJSON> Execute(RegisterUserRequestJSON request)
    {
        await ValidateAsync(request);

        var user = map.Map<Domain.Entities.User>(request);
        user.Password = service.Encrypt(request.Password);

        await writeRepo.AddUserAsync(user);

        await uw.CommitAsync();

        return new RegisterUserResponseJSON(user.Name);
    }

    private async Task ValidateAsync(RegisterUserRequestJSON request)
    {
        var result = await new RegisterUserValidator().ValidateAsync(request);

        var emailExists = await readRepo.ExistsActiveUserWithEmailAsync(request.Email);

        if (emailExists)
            result.Errors.Add(new(string.Empty,
                ResourcesAccessor.EMAIL_ALREADY_REGISTERED));

        if (result.IsValid)
            return;
        var errorMessages = (from errors in result.Errors select errors.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}