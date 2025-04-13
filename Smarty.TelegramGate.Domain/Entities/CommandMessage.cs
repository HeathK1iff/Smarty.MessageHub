namespace Smarty.TelegramGate.Domain.Entities;

public sealed class CommandMessage : MessageContainer
{
    public CommandMessage(MessageBase @base) : base(@base)
    {
    }

    public required string Name { get; init; }
    public required string[] Params { get; init; }
    public MessageBase? Previous { get; init; }
}