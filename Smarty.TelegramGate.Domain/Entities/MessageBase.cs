namespace Smarty.TelegramGate.Domain.Entities;

public abstract class MessageBase : EntityBase
{
    public long SessionId { get; init; }
    public Guid? UserId { get; init; }
}
