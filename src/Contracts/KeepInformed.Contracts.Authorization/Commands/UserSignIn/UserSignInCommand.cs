using MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignIn;

public class UserSignInCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
