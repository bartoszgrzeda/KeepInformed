using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Queries.GetUserJwt;

public class GetUserJwtQuery : IMasterQuery<GetUserJwtQueryResponse>
{
    public string Email { get; set; }
}
