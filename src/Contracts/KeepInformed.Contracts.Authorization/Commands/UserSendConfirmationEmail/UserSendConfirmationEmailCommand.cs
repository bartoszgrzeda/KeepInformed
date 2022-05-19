using MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSendConfirmationEmail;

public class UserSendConfirmationEmailCommand : IRequest
{
    public Guid UserId { get; set; }
}
