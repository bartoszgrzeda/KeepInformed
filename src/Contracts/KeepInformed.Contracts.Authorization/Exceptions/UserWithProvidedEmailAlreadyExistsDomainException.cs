using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserWithProvidedEmailAlreadyExistsDomainException : DomainException
{
    public UserWithProvidedEmailAlreadyExistsDomainException() : base("USER_WITH_PROVIDED_EMAIL_ALREADY_EXISTS")
    {
    }
}
