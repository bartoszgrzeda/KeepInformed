using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Application.Authorization.Services;
using KeepInformed.Common.EventBus;
using KeepInformed.Contracts.Authorization.Commands.UserSignUp;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using KeepInformed.Domain.Authorization.Entities;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserSignUp;

public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncrypter _encrypter;
    private readonly IEventBus _eventBus;

    public UserSignUpCommandHandler(IUserRepository userRepository, IEncrypter encrypter, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _encrypter = encrypter;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.ToLower();

        var existingUser = await _userRepository.GetByEmail(email);

        if (existingUser != null)
        {
            throw new UserWithProvidedEmailAlreadyExistsDomainException();
        }

        var salt = _encrypter.GetSalt();
        var passwordHash = _encrypter.GetHash(request.Password, salt);

        var user = new User(email, passwordHash, salt);

        await _userRepository.Add(user);
        await _userRepository.SaveChanges();

        var userSignedUp = new UserSignedUp()
        {
            Email = email
        };

        _eventBus.Publish(userSignedUp);

        return Unit.Value;
    }
}
