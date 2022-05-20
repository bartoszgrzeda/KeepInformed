using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserEmailNotConfirmedDomainException : DomainException
{
    public UserEmailNotConfirmedDomainException() : base("USER_EMAIL_NOT_CONFIRMED")
    {
    }
}
