namespace Smarty.TelegramGate.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
