using KeepInformed.Common.EventHandlers;
using KeepInformed.Contracts.Authorization.IntegrationEvents;

namespace KeepInformed.Web.Jobs.IntegrationEventHandlers;

public class SendUserSignedUpConfirmationEmail : IIntegrationEventHandler<UserSignedUp>
{
    public async Task Handle(UserSignedUp integrationEvent)
    {
        var a = 5;
    }
}
