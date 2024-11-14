using FluentValidation;
using RecipesApp.Communication.Requests;
using RecipesApp.Exception.Resources;

namespace RecipesApp.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestJSON>
{
    public RegisterUserValidator()
    {
        RuleFor((u) => u.Name).NotNull().NotEmpty()
            .WithMessage(ResourcesAccessor.NAME_REQUIRED);

        RuleFor((u) => u.Email).NotNull().NotEmpty()
            .WithMessage(ResourcesAccessor.EMAIL_REQUIRED);
        RuleFor((u) => u.Email).EmailAddress()
            .WithMessage(ResourcesAccessor.EMAIL_INVALID);

        RuleFor(u => u.Password).NotNull().NotEmpty()
            .WithMessage(ResourcesAccessor.PASSWORD_REQUIRED);
        RuleFor((u) => u.Password.Length).GreaterThanOrEqualTo(6)
            .WithMessage(ResourcesAccessor.PASSWORD_LENGTH);
    }
}
