using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Domain.Entities;

public class Message : MessageBase, IMessageData
{
    public string? MessageData { get; init; }
}
