using RabbitMQ.Client;

namespace KeepInformed.Infrastructure.RabbitMq;

public interface IRabbitMqConnection
{
    bool IsConnected { get; }

    void Connect();
    IModel CreateChannel();
}
