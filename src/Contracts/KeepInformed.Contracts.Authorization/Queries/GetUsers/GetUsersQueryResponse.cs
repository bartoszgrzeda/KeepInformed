using KeepInformed.Contracts.Authorization.Dto;

namespace KeepInformed.Contracts.Authorization.Queries.GetUsers;

public class GetUsersQueryResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}
