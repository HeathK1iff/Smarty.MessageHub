using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Domain.Entities;

public sealed class ResponseMessage : MessageContainer, IMessageData
{
    public string? MessageData { get; set; }

    public ResponseMessage(MessageBase source) : base(source)
    {
    }
}
