namespace Smarty.TelegramGate.Domain.Entities;

public class MessageBase : EntityBase, IUserIdAssigner
{
    protected Guid? _userId;
    readonly Dictionary<string, string> _customProps = new();

    public string? Body { get; init; }
    public DateTime Created { get; } = DateTime.UtcNow;
    public Guid? UserId { get => _userId; }
    public Dictionary<string, string> CustomProperties => _customProps;

    public MessageBase(MessageBase @base): base()
    {       
        Body = @base.Body;
        _userId = @base.UserId;
        
        foreach (KeyValuePair<string, string> entry in @base.CustomProperties)
        {
            CustomProperties.Add(entry.Key, entry.Value);
        }
    }

    public MessageBase()
    {

    }
    
    void IUserIdAssigner.SetUserId(Guid userId)
    {
        _userId = userId;
    }
}


internal interface IUserIdAssigner
{
    void SetUserId(Guid userId);
}

