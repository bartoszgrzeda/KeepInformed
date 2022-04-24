using MediatR;

namespace KeepInformed.Contracts.Authorization.Commands.UserSignUp;

public class UserSignUpCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
