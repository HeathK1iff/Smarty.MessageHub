namespace Smarty.TelegramGate.Domain.Entities;

public enum RecipientType { Telegram }

public sealed class Message : MessageBase
{
    public Message(MessageBase messageBase) : base(messageBase)
    {       

    }
}