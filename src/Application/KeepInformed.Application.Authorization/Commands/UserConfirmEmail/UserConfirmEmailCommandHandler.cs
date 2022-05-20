using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Contracts.Authorization.Commands.UserConfirmEmail;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Domain.Authorization.Entities;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserConfirmEmail;

public class UserConfirmEmailCommandHandler : IRequestHandler<UserConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserEmailConfirmationRepository _userEmailConfirmationRepository;

    public UserConfirmEmailCommandHandler(IUserRepository userRepository, IUserEmailConfirmationRepository userEmailConfirmationRepository)
    {
        _userRepository = userRepository;
        _userEmailConfirmationRepository = userEmailConfirmationRepository;
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
