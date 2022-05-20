using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserEmailConfirmationNotFoundDomainException : DomainException
{
    public UserEmailConfirmationNotFoundDomainException() : base("USER_EMAIL_CONFIRMATION_NOT_FOUND")
    {
    }
}
