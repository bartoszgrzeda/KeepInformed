using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace KeepInformed.Infrastructure.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection? _connection;

    public RabbitMqConnection(IConfiguration configuration)
    {
        var hostName = configuration["RabbitMq:HostName"];

        _connectionFactory = new ConnectionFactory()
        {
            HostName = hostName,
            DispatchConsumersAsync = true
        };
    }

    public bool IsConnected => _connection?.IsOpen ?? false;

    public void Connect()
    {
        _connection = _connectionFactory.CreateConnection();
    }

    public IModel CreateChannel()
    {
        return _connection.CreateModel();
    }
}
