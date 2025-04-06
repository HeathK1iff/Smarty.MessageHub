using Smarty.TelegramGate.Domain.Repositories;

namespace Smarty.TelegramGate.Domain;

public interface IAccessValidationService
{
    bool HasAccess(Guid messageUserId, Permission permission);
}


