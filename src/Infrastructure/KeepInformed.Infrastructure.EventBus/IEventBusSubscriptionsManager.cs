using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;

namespace KeepInformed.Infrastructure.EventBus;

public interface IEventBusSubscriptionsManager
{
    void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
       where TIntegrationEvent : IntegrationEvent
       where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

    public Subscription? GetSubscriptionByEventName(string eventTypeName);
    public Subscription? GetSubscriptionByEventType<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent;
}
