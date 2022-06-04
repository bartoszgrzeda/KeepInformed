using KeepInformed.Common.Events;

namespace KeepInformed.Contracts.TenantNews.IntegrationEvents;

public class TvnNewsScheduledToBeSynchronized : IntegrationEvent
{
    public Guid UserId { get; set; }
}
