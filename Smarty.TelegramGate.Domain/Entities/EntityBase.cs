namespace Smarty.TelegramGate.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; }

    public EntityBase(Guid id)
    {
        Id = id;
    }

    public EntityBase() : this(Guid.NewGuid())
    {

    }
}
