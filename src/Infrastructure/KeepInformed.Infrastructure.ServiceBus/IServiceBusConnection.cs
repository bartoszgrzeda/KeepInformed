using Azure.Messaging.ServiceBus;

namespace KeepInformed.Infrastructure.ServiceBus;

public interface IServiceBusConnection
{
    ServiceBusSender GetSender();     
}
