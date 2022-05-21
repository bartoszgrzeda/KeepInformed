using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Common.EventBus;
using KeepInformed.Contracts.Authorization.Commands.UserConfirmEmail;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using KeepInformed.Domain.Authorization.Entities;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserConfirmEmail;

public class UserConfirmEmailCommandHandler : IRequestHandler<UserConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserEmailConfirmationRepository _userEmailConfirmationRepository;
    private readonly IEventBus _eventBus;

    public UserConfirmEmailCommandHandler(IUserRepository userRepository, IUserEmailConfirmationRepository userEmailConfirmationRepository, IEventBus eventBus)
    {
        _userRepository = userRepository;
        _userEmailConfirmationRepository = userEmailConfirmationRepository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UserConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var confirmationId = request.ConfirmationId;

        var user = await _userRepository.Get(userId);

        ValidateUser(user);

        var confirmation = await _userEmailConfirmationRepository.GetActiveByIdAndUserId(confirmationId, userId);

        ValidateConfirmation(confirmation);

        user.SetIsEmailConfirmed(true);
        confirmation.SetIsActive(false);

        _userRepository.Update(user);
        _userEmailConfirmationRepository.Update(confirmation);

        await _userRepository.SaveChanges();

        var userConfirmedEmail = new UserConfirmedEmail()
        {
            UserId = userId
        };
        _eventBus.Publish(userConfirmedEmail);

        return Unit.Value;
    }

    private void ValidateUser(User? user)
    {
        if (user == null)
        {
            throw new UserWithProvidedIdNotFoundDomainException();
        }

        if (user.IsEmailConfirmed)
        {
            throw new UserEmailAlreadyConfirmedDomainException();
        }
    }

    private void ValidateConfirmation(UserEmailConfirmation? confirmation)
    {
        if (confirmation == null)
        {
            throw new UserEmailConfirmationNotFoundDomainException();
        }

        if (confirmation.CreatedDate.AddMinutes(10) < DateTime.UtcNow)
        {
            throw new UserEmailConfirmationExpiredDomainException();
        }
    }
}
