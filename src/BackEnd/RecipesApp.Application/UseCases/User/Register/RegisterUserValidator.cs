using FluentValidation;
using RecipesApp.Communication.Requests;

namespace RecipesApp.Application.UseCases.User.Register;

internal class RegisterUserValidator : AbstractValidator<RegisterUserRequestJSON>
{
    internal RegisterUserValidator()
    {
        RuleFor((u) => u.Name).NotNull().NotEmpty().WithMessage("The Name field is required");

        RuleFor((u) => u.Email).NotNull().NotEmpty().WithMessage("The Email field is required");
        RuleFor((u) => u.Email).EmailAddress().WithMessage("Email isn't valid");

        RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("The Password field is required");
        RuleFor((u) => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Password must be at least 6 characters along");
    }
}
