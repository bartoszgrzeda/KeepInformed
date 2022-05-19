using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Common.MailMessage;
using KeepInformed.Contracts.Authorization.Commands.UserSendConfirmationEmail;
using KeepInformed.Contracts.Authorization.Exceptions;
using KeepInformed.Domain.Authorization.Entities;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserSendConfirmationEmail;

public class UserSendConfirmationEmailCommandHandler : IRequestHandler<UserSendConfirmationEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSignedUpConfirmationRepository _userSignedUpConfirmationRepository;
    private readonly IMailMessageBuilder _mailMessageBuilder;
    private readonly IMailMessageSender _mailMessageSender;

    public UserSendConfirmationEmailCommandHandler(IUserRepository userRepository, IUserSignedUpConfirmationRepository userSignedUpConfirmationRepository, IMailMessageBuilder mailMessageBuilder, IMailMessageSender mailMessageSender)
    {
        _userRepository = userRepository;
        _userSignedUpConfirmationRepository = userSignedUpConfirmationRepository;
        _mailMessageBuilder = mailMessageBuilder;
        _mailMessageSender = mailMessageSender;
    }

    public async Task<Unit> Handle(UserSendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new UserWithProvidedIdNotFoundDomainException();
        }

        var activeConfirmation = await _userSignedUpConfirmationRepository.GetActiveByUserId(userId);

        if (activeConfirmation != null)
        {
            activeConfirmation.SetIsActive(false);
            _userSignedUpConfirmationRepository.Update(activeConfirmation);
        }

        var confirmationId = Guid.NewGuid();
        var confirmation = new UserSignedUpConfirmation(confirmationId, userId);

        await _userSignedUpConfirmationRepository.Add(confirmation);

        // TO DO get template from db
        var subject = "Keep Informed | Confirm your email address";
        var body = $"UserId: {userId}\nConfirmationId: {confirmationId}";
        var receiver = user.Email;

        var message = _mailMessageBuilder.Create()
            .AddSubject(subject)
            .AddBody(body)
            .AddReceiver(receiver)
            .Build();

        await _mailMessageSender.Send(message);

        await _userSignedUpConfirmationRepository.SaveChanges();

        return Unit.Value;
    }
}
