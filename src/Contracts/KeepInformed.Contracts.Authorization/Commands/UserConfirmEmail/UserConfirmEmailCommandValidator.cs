using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Commands.UserConfirmEmail;

public class UserConfirmEmailCommandValidator : AbstractValidator<UserConfirmEmailCommand>
{
    public UserConfirmEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);

        RuleFor(x => x.ConfirmationId)
            .NotEqual(Guid.Empty);
    }
}
