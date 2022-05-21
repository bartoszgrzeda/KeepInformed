using KeepInformed.Common.EventBus;
using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;

namespace KeepInformed.Tests.UnitTests.Mocks;

public class EventBusMock : IEventBus
{
    public void Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
    {
    }

    public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
    {
    }
}
