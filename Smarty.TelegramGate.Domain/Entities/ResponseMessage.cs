using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Entities;

public sealed class ResponseMessage : MessageContainer, IMessageData
{
    public string? MessageData { get; set; }

    public ResponseMessage(MessageBase source) : base(source)
    {
    }
}
