using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignIn;

public class UserSignInCommandValidator : AbstractValidator<UserSignInCommand>
{
    public UserSignInCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
