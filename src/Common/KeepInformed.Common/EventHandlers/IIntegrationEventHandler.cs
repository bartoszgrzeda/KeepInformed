using KeepInformed.Common.Events;

namespace KeepInformed.Common.EventHandlers;

public interface IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent integrationEvent);
}
