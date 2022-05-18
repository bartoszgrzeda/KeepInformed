namespace KeepInformed.Infrastructure.EventBus;

public class Subscription
{
    public Type EventType { get; private set; }
    public List<Type> HandlerTypes { get; private set; }

    public Subscription(Type eventType, List<Type> handlerTypes)
    {
        EventType = eventType;
        HandlerTypes = handlerTypes;
    }

    public void AddHandlerType(Type handlerType)
    {
        if (HandlerTypes.Contains(handlerType))
        {
            return;
        }

        HandlerTypes.Add(handlerType);
    }
}
