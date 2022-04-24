using KeepInformed.Application.Authorization.Services;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Contracts.Authorization.Queries.GetUserJwt;
using MediatR;

namespace KeepInformed.Application.Authorization.Queries.GetUserJwt;

public class GetUserJwtQueryHandler : IRequestHandler<GetUserJwtQuery, GetUserJwtQueryResponse>
{
    private readonly IJwtTokenService _jwtTokenService;

    public GetUserJwtQueryHandler(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public async Task<GetUserJwtQueryResponse> Handle(GetUserJwtQuery request, CancellationToken cancellationToken)
    {
        var jwtToken = _jwtTokenService.GetFromCache(request.Email);

        if (jwtToken == null)
        {
            throw new JwtTokenNotFoundDomainException();
        }

        return new GetUserJwtQueryResponse()
        {
            ExpirationDate = jwtToken.ExpirationDate,
            Token = jwtToken.Token
        };
    }
}
