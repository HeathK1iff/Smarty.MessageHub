namespace Smarty.TelegramGate.Domain.Entities;

public sealed class User : EntityBase
{
    public required UserRole[] Roles { get; init; }
}
