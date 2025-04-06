namespace Smarty.TelegramGate.Domain.Entities;

public class MessageBase : EntityBase
{
    public Guid UserId { get; init; }
  
    protected MessageBase()
    {

    }

    public static MessageBase Empty()
    {
        return new MessageBase();
    }
}
