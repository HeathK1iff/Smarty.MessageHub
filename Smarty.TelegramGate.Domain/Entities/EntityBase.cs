namespace Smarty.TelegramGate.Domain.Entities;

public abstract class EntityBase: ICloneable
{
    public Guid Id { get; init; }

    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}
