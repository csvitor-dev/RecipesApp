using FluentValidation;
using RecipesApp.Communication.Requests;
using RecipesApp.Exception.Resources;

namespace RecipesApp.Application.UseCases.User.Login.DoLogin;

public class DoLoginValidator : AbstractValidator<LoginUserRequestJSON>
{
    public DoLoginValidator()
    {
        RuleFor(u => u.Email).NotNull().NotEmpty()
            .WithMessage(ResourcesAccessor.EMAIL_REQUIRED);

        When(u => string.IsNullOrWhiteSpace(u.Email) is false, ()
            => RuleFor(u => u.Email).EmailAddress()
                .WithMessage(ResourcesAccessor.EMAIL_INVALID)
        );

        RuleFor(u => u.Password).NotNull().NotEmpty()
            .WithMessage(ResourcesAccessor.PASSWORD_REQUIRED);
        
        When(u => string.IsNullOrWhiteSpace(u.Password) is false, ()
            => RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6)
                .WithMessage(ResourcesAccessor.PASSWORD_LENGTH)
        );
    }
}