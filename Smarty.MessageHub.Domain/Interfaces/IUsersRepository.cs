using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IUserRepository
{
    void InsertOrUpdate(User user);

    User[] FindByContactInformation(string contact);
}
