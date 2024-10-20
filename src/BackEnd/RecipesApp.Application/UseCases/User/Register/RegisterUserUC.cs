using AutoMapper;
using RecipesApp.Application.Services;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;
using RecipesApp.Domain.Repositories.User;
using RecipesApp.Exception.Project;

namespace RecipesApp.Application.UseCases.User.Register;

public class RegisterUserUC(IUserReadOnlyRepository readRepo, IUserWriteOnlyRepository writeRepo)
{
    private readonly RegisterUserValidator _validator = new();
    private readonly IUserReadOnlyRepository _readRepo = readRepo;
    private readonly IUserWriteOnlyRepository _writeRepo = writeRepo;

    public async Task<RegisterUserResponseJSON> Execute(RegisterUserRequestJSON request)
    {
        Validate(request);

        IMapper map = new MapperConfiguration((opt) =>
        {
            opt.AddProfile(new AutoMappingService());
        }).CreateMapper();
        PasswordEncryptionService pw = new();

        var user = map.Map<Domain.Entities.User>(request);
        user.Password = pw.Encrypt(request.Password);

        await _writeRepo.AddUserAsync(user);

        return new(request.Name);
    }
    private void Validate(RegisterUserRequestJSON request)
    {
        var result = _validator.Validate(request);

        if (result.IsValid)
            return;
        var errorMessages = (from errors in result.Errors select errors.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}
