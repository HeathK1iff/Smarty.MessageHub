namespace Smarty.TelegramGate.Domain.Entities;

public sealed class ResponseMessage : MessageContainer
{
    public required string Message { get; set; }
    
    public ResponseMessage(MessageBase source) : base(source)
    {
    }
}
