namespace Smarty.MessageHub.Domain.Entities;

public abstract class MessageContainer : MessageBase
{
    public MessageContainer(MessageBase @base)
    {
        Base = @base;
    }
    
    public MessageBase Base { get; private set; }
}
