using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IUsersRepository
{
    Task InsertOrUpdateAsync(User user);
}
