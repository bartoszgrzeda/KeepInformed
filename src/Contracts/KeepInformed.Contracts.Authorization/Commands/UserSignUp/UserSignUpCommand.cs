using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignUp;

public class UserSignUpCommand : IMasterCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}
