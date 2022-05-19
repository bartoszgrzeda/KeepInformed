using KeepInformed.Common.Events;

namespace KeepInformed.Contracts.Authorization.IntegrationEvents;

public class UserSignedUp : IntegrationEvent
{
    public Guid UserId { get; set; }
}
