using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Entities;

public class Message : MessageBase, IMessageData
{
    public string? MessageData { get; init; }
}
