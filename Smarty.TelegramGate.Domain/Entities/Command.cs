namespace Smarty.TelegramGate.Domain.Entities;

public sealed class Command : MessageBase
{
    public Command(MessageBase message) : base(message)
    {
    }

    public required string CommandName { get; init; }
    public required string[] Params { get; init; }
    public MessageBase? Previous { get; set; }
}