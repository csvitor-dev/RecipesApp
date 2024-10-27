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
    private readonly IUserReadOnlyRepository _readRepo = readRepo;
    private readonly IUserWriteOnlyRepository _writeRepo = writeRepo;
    private readonly IUnitOfWork _uw = uw;
    private readonly IMapper _map = map;
    private readonly PasswordEncryptionService _service = service;

    public async Task<RegisterUserResponseJSON> Execute(RegisterUserRequestJSON request)
    {
        await Validate(request);

        var user = _map.Map<Domain.Entities.User>(request);
        user.Password = _service.Encrypt(request.Password);

        await _writeRepo.AddUserAsync(user);
       
        await _uw.CommitAsync();

        return new(request.Name);
    }
    private async Task Validate(RegisterUserRequestJSON request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExists = await _readRepo.ExistsActiveUserWithEmailAsync(request.Email);

        if (emailExists)
            result.Errors.Add(new(string.Empty,
                ResourcesAccessor.EMAIL_ALREADY_REGISTERED));

        if (result.IsValid)
            return;
        var errorMessages = (from errors in result.Errors select errors.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}
