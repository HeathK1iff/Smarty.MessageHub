namespace Smarty.TelegramGate.Domain.Entities;

public class Message : MessageBase
{
    public string? Body { get; init; }
    public DateTime Created { get; } = DateTime.UtcNow;  
}
