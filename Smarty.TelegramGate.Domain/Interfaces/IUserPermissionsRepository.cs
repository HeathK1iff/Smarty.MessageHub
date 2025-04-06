using Smarty.TelegramGate.Domain.Repositories;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IUserPermissionsRepository
{
    Permission[] GetPermissionsOrDefault(Guid userId);
}
