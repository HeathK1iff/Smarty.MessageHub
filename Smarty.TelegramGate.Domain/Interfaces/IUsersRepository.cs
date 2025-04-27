using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IUserRepository
{
    void InsertOrUpdate(User user);

    User[] FindByContactInformation(string contact);
}
