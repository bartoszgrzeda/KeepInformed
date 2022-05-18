namespace KeepInformed.Common.Events;

public abstract class IntegrationEvent
{
    public Guid Id { get; private set; }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
    }
}
