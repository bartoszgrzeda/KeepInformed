using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Application.Authorization.Services;
using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Exceptions;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserSignIn;

public class UserSignInCommandHandler : IRequestHandler<UserSignInCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncrypter _encrypter;
    private readonly IJwtTokenService _jwtTokenService;

    public UserSignInCommandHandler(IUserRepository userRepository, IEncrypter encrypter, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _encrypter = encrypter;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Unit> Handle(UserSignInCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.ToLower();

        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            throw new UserInvalidCredentialsDomainException();
        }

        var hashedPassword = _encrypter.GetHash(request.Password, user.Salt);

        if (user.Password != hashedPassword)
        {
            throw new UserInvalidCredentialsDomainException();
        }

        user.SetLastSignInDate(DateTime.UtcNow);
        _userRepository.Update(user);
        await _userRepository.SaveChanges();

        var jwtToken = _jwtTokenService.GenerateJwtToken(user);
        _jwtTokenService.SetInCache(email, jwtToken);

        return Unit.Value;
    }
}
