using KeepInformed.Common.Events;

namespace KeepInformed.Contracts.Authorization.IntegrationEvents;

public class UserSignedUp : IntegrationEvent
{
    public string Email { get; set; }
}
