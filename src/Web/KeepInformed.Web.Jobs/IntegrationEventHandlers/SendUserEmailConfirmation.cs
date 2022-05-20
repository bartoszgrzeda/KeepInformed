using KeepInformed.Common.EventHandlers;
using KeepInformed.Contracts.Authorization.Commands.UserSendConfirmationEmail;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using MediatR;

namespace KeepInformed.Web.Jobs.IntegrationEventHandlers;

public class SendUserEmailConfirmation : IIntegrationEventHandler<UserSignedUp>
{
    private readonly IMediator _mediator;

    public SendUserEmailConfirmation(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserSignedUp integrationEvent)
    {
        var command = new UserSendConfirmationEmailCommand()
        {
            UserId = integrationEvent.UserId
        };

        await _mediator.Send(command);
    }
}
