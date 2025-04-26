using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Infrastructure;

public class UserRepository : IUsersRepository
{
    public Task InsertOrUpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}
