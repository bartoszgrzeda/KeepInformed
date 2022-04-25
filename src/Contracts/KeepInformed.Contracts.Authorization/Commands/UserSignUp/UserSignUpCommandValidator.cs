using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignUp;

public class UserSignUpCommandValidator : AbstractValidator<UserSignUpCommand>
{
    public UserSignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
