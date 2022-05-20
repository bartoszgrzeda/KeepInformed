using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserEmailAlreadyConfirmedDomainException : DomainException
{
    public UserEmailAlreadyConfirmedDomainException() : base("USER_EMAIL_ALREADY_CONFIRMED")
    {
    }
}
