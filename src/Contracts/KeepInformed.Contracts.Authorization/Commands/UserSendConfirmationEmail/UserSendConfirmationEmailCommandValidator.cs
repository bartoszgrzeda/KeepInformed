using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Commands.UserSendConfirmationEmail;

public class UserSendConfirmationEmailCommandValidator : AbstractValidator<UserSendConfirmationEmailCommand>
{
    public UserSendConfirmationEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}
