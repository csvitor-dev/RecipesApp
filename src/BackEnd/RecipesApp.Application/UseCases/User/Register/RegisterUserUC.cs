using AutoMapper;
using RecipesApp.Application.Services;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Exception.Project;

namespace RecipesApp.Application.UseCases.User.Register;

public class RegisterUserUC(
    IUserReadOnlyRepository readRepo,
    IUserWriteOnlyRepository writeRepo,
    IMapper map,
    PasswordEncryptionService service
) : IRegisterUserUC
{
    private readonly IUserReadOnlyRepository _readRepo = readRepo;
    private readonly IUserWriteOnlyRepository _writeRepo = writeRepo;
    private readonly IMapper _map = map;
    private readonly PasswordEncryptionService _service = service;

    public async Task<RegisterUserResponseJSON> Execute(RegisterUserRequestJSON request)
    {
        Validate(request);

        var user = _map.Map<Domain.Entities.User>(request);
        user.Password = _service.Encrypt(request.Password);

        await _writeRepo.AddUserAsync(user);

        return new(request.Name);
    }
    private void Validate(RegisterUserRequestJSON request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result.IsValid)
            return;
        var errorMessages = (from errors in result.Errors select errors.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}
