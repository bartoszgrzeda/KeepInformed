using KeepInformed.Common.EventBus;
using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.Events;
using KeepInformed.Infrastructure.EventBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace KeepInformed.Infrastructure.RabbitMq;

public class RabbitMqEventBus : IEventBus
{
    private const string EXCHANGE_NAME = "KeepInformedExchange";
    private const string QUEUE_NAME = "KeepInformedQueue";

    private readonly IRabbitMqConnection _connection;
    private readonly IEventBusSubscriptionsManager _subscriptionsManager;
    private readonly IServiceProvider _serviceProvider;

    private IModel? _consumerChannel;

    public RabbitMqEventBus(IRabbitMqConnection connection, IEventBusSubscriptionsManager subscriptionsManager, IServiceProvider serviceProvider)
    {
        _connection = connection;
        _subscriptionsManager = subscriptionsManager;
        _serviceProvider = serviceProvider;
    }

    public void Publish(IntegrationEvent @event)
    {
        if (!_connection.IsConnected)
        {
            _connection.Connect();
        }

        using var channel = _connection.CreateChannel();

        channel.ExchangeDeclare(exchange: EXCHANGE_NAME,
            type: ExchangeType.Direct);

        var serializedEvent = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(serializedEvent);
        var eventTypeName = @event.GetType().FullName;

        channel.BasicPublish(exchange: EXCHANGE_NAME,
            routingKey: eventTypeName,
            body: body);
    }

    public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
    {
        if (!_connection.IsConnected)
        {
            _connection.Connect();
        }

        if (_consumerChannel == null)
        {
            CreateConsumerChannel();
        }

        var eventTypeName = typeof(TIntegrationEvent).FullName;

        _consumerChannel.QueueBind(queue: QUEUE_NAME,
            exchange: EXCHANGE_NAME,
            routingKey: eventTypeName);

        _subscriptionsManager.AddSubscription<TIntegrationEvent, TIntegrationEventHandler>();

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

        consumer.Received += HandleEvents;

        _consumerChannel.BasicConsume(queue: QUEUE_NAME,
            autoAck: false,
            consumer: consumer);
    }

    private void CreateConsumerChannel()
    {
        _consumerChannel = _connection.CreateChannel();

        _consumerChannel.ExchangeDeclare(exchange: EXCHANGE_NAME,
            type: ExchangeType.Direct);

        _consumerChannel.QueueDeclare(queue: QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    private async Task HandleEvents(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventTypeName = eventArgs.RoutingKey;
        var subscription = _subscriptionsManager.GetSubscriptionByEventName(eventTypeName);

        if (subscription == null)
        {
            return;
        }

        var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
        var eventType = subscription.EventType;

        foreach (var handlerType in subscription.HandlerTypes)
        {
            await HandleEvent(eventType, handlerType, body);
        }

        _consumerChannel!.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }

    private async Task HandleEvent(Type eventType, Type handlerType, string body)
    {
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            return;
        }

        var integrationEvent = JsonSerializer.Deserialize(body, eventType);

        await (Task)handlerType.GetMethod("Handle")!.Invoke(handler, new object[] { integrationEvent })!;
    }
}
