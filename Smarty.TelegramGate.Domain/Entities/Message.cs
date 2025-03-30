namespace Smarty.TelegramGate.Domain.Entities;

public sealed class Message : MessageBase
{
    public string? Body { get; init; }
    public DateTime Created { get; init; }
}
