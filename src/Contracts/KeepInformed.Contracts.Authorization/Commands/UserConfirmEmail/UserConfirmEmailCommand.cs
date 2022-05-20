using MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserConfirmEmail;

public class UserConfirmEmailCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ConfirmationId { get; set; }
}
