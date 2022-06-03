using Azure.Messaging.ServiceBus;
using KeepInformed.Common.EventBus;
using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;
using System.Text;
using System.Text.Json;

namespace KeepInformed.Infrastructure.ServiceBus;

public class ServiceBusEventBus : IEventBus
{
    private readonly IServiceBusConnection _connection;

    public ServiceBusEventBus(IServiceBusConnection connection)
    {
        _connection = connection;
    }

    public void Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
    {
        var sender = _connection.GetSender();

        var serializedEvent = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(serializedEvent);
        var eventTypeName = @event.GetType().FullName;

        var message = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = eventTypeName
        };

        sender.SendMessageAsync(message).GetAwaiter().GetResult();
    }

    public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
    {
        throw new NotImplementedException();
    }
}
