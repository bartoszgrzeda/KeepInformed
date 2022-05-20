using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserEmailConfirmationExpiredDomainException : DomainException
{
    public UserEmailConfirmationExpiredDomainException() : base("USER_EMAIL_CONFIRMATION_EXPIRED")
    {
    }
}
