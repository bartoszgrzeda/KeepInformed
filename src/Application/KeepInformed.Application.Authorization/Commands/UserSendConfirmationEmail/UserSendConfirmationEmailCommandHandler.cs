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
    private readonly IUserEmailConfirmationRepository _userEmailConfirmationRepository;
    private readonly IMailMessageBuilder _mailMessageBuilder;
    private readonly IMailMessageSender _mailMessageSender;

    public UserSendConfirmationEmailCommandHandler(IUserRepository userRepository, IUserEmailConfirmationRepository userEmailConfirmationRepository, IMailMessageBuilder mailMessageBuilder, IMailMessageSender mailMessageSender)
    {
        _userRepository = userRepository;
        _userEmailConfirmationRepository = userEmailConfirmationRepository;
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

        var activeConfirmation = await _userEmailConfirmationRepository.GetActiveByUserId(userId);

        if (activeConfirmation != null)
        {
            activeConfirmation.SetIsActive(false);
            _userEmailConfirmationRepository.Update(activeConfirmation);
        }

        var confirmationId = Guid.NewGuid();
        var confirmation = new UserEmailConfirmation(confirmationId, userId);

        await _userEmailConfirmationRepository.Add(confirmation);

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

        await _userEmailConfirmationRepository.SaveChanges();

        return Unit.Value;
    }
}
