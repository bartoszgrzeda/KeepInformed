using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserConfirmEmail;

public class UserConfirmEmailCommand : IMasterCommand
{
    public Guid UserId { get; set; }
    public Guid ConfirmationId { get; set; }
}
