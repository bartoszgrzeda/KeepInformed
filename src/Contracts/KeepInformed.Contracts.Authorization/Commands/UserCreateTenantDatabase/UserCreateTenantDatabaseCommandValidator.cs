using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Commands.UserCreateTenantDatabase;

public class UserCreateTenantDatabaseCommandValidator : AbstractValidator<UserCreateTenantDatabaseCommand>
{
    public UserCreateTenantDatabaseCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}
