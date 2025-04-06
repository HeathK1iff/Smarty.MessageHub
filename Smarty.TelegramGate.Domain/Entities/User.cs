using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Repositories;

public sealed class User : EntityBase
{
    public required UserRole[] Roles { get; init; }
}
