namespace Smarty.TelegramGate.Domain.Entities;

public class MessageBase : EntityBase
{
    public Guid UserId { get; init; }
    public Dictionary<string, string> Params = new ();

    protected MessageBase()
    {

    }

    public static MessageBase Empty()
    {
        return new MessageBase();
    }
}
