using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Repositories;

public sealed class UserRole : EntityBase
{
    public required Permission[] Permissions { get; init; } 
}
