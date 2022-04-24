using MediatR;

namespace KeepInformed.Contracts.Authorization.Queries.GetUserJwt;

public class GetUserJwtQuery : IRequest<GetUserJwtQueryResponse>
{
    public string Email { get; set; }
}
