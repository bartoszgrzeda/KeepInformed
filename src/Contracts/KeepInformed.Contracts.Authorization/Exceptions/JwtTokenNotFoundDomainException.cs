using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.Authorization.Exceptions;

public class JwtTokenNotFoundDomainException : DomainException
{
    public JwtTokenNotFoundDomainException() : base("JWT_TOKEN_NOT_FOUND")
    {
    }
}
