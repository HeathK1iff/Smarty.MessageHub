using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Infrastructure.Repository;

public sealed class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration, IMemoryCache memoryCache) 
        : base(memoryCache, 
              configuration?.GetConnectionString("UsersDb") ?? throw new ArgumentException(nameof(configuration)))
    {
    }

    public User[] FindByContactInformation(string contactInformation)
    {
        return GetAllOrEmpty()
            .SelectMany(a => a?.Contacts ?? Array.Empty<UserContact>() , (a,b) => 
            new {
                Contact = a,
                Information = b.ContactInformation
            }).Where(a => a.Information == contactInformation)
            .Select(a => a.Contact)
            .ToArray();
    }

    public new void InsertOrUpdate(User user)
    {
        base.InsertOrUpdate(user);
    }
}
