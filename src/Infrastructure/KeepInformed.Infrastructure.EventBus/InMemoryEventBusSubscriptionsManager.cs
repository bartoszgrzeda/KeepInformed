using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;

namespace KeepInformed.Infrastructure.EventBus;

public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    private readonly List<Subscription> _subscriptions;

    public InMemoryEventBusSubscriptionsManager()
    {
        _subscriptions = new List<Subscription>();
    }

    public void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
    {
        var eventType = typeof(TIntegrationEvent);
        var handlerType = typeof(TIntegrationEventHandler);

        var subscription = GetSubscriptionByEventType<TIntegrationEvent>();

        if (subscription == null)
        {
            AddNewSubscription(eventType, handlerType);
        }

        else
        {
            subscription.AddHandlerType(handlerType);
        }
    }

    private void AddNewSubscription(Type eventType, Type handlerType)
    {
        var handlers = new List<Type>();
        handlers.Add(handlerType);

        var subscription = new Subscription(eventType, handlers);

        _subscriptions.Add(subscription);
    }

    public Subscription? GetSubscriptionByEventName(string eventTypeName)
    {
        return _subscriptions.SingleOrDefault(x => x.EventType.FullName == eventTypeName);
    }

    public Subscription? GetSubscriptionByEventType<TIntegrationEvent>() where TIntegrationEvent : IntegrationEvent
    {
        return _subscriptions.SingleOrDefault(x => x.EventType == typeof(TIntegrationEvent));
    }
}
