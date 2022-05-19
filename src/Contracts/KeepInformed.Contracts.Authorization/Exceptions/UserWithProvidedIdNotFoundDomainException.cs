using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class UserWithProvidedIdNotFoundDomainException : DomainException
{
    public UserWithProvidedIdNotFoundDomainException() : base("USER_WITH_PROVIDED_ID_NOT_FOUND")
    {
    }
}
