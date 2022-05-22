using KeepInformed.Common.Events;

namespace KeepInformed.Contracts.TenantNews.IntegrationEvents;

public class SynchronizeTvnNews : IntegrationEvent
{
    public Guid UserId { get; set; }
}
