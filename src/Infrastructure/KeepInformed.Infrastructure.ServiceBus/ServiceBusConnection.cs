using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace KeepInformed.Infrastructure.ServiceBus;

public class ServiceBusConnection : IServiceBusConnection
{
    private readonly string _topicName;
    private readonly string _connectionString;

    private ServiceBusClient? _topicClient;

    private bool IsTopicClientClosed => _topicClient?.IsClosed ?? true;

    public ServiceBusConnection(IConfiguration configuration)
    {
        _topicName = configuration["ServiceBus:TopicName"];
        _connectionString = configuration["ServiceBus:ConnectionString"];
    }

    public ServiceBusSender GetSender()
    {
        if (IsTopicClientClosed)
        {
            _topicClient = new ServiceBusClient(_connectionString);
        }

        return _topicClient!.CreateSender(_topicName);
    }
}
