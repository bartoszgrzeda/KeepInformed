namespace KeepInformed.Common.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public BaseEntity(Guid id)
    {
        Id = id;
        CreatedDate = DateTime.UtcNow;
    }
}
