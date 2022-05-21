using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSendConfirmationEmail;

public class UserSendConfirmationEmailCommand : IMasterCommand
{
    public Guid UserId { get; set; }
}
