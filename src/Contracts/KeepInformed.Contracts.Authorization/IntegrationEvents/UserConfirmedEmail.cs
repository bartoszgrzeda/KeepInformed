using KeepInformed.Common.Events;

namespace KeepInformed.Contracts.Authorization.IntegrationEvents;

public class UserConfirmedEmail : IntegrationEvent
{
    public Guid UserId { get; set; }
}
