namespace Smarty.MessageHub.Domain.Entities;

public sealed class UserContact: ICloneable
{
    public ContactType Type { get; init; }
    public string? ContactInformation { get; init; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
