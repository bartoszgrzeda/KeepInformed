using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserInvalidCredentialsDomainException : DomainException
{
    public UserInvalidCredentialsDomainException() : base("USER_INVALID_CREDENTIALS")
    {
    }
}
