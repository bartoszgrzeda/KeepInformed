using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserCreateTenantDatabase;

public class UserCreateTenantDatabaseCommand : IMasterCommand
{
    public Guid UserId { get; set; }
}
