namespace Smarty.TelegramGate.Domain.Entities;

public sealed class Command : MessageBase
{
    public required string CommandName { get; init; }
    public required string[] Params {get; init; }
}