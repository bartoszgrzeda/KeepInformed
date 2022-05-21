using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignIn;

public class UserSignInCommand : IMasterCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}
