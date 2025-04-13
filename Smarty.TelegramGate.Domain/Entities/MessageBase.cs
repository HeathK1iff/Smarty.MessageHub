namespace Smarty.TelegramGate.Domain.Entities;

public abstract class MessageBase : EntityBase
{
    public DateTime Created { get; init; } = DateTime.UtcNow;
}
