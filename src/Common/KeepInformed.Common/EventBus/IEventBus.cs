using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;

namespace KeepInformed.Common.EventBus;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;
}
