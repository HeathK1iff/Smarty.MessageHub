using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Smarty.MessageHub.Infrastructure.Interfaces;

namespace Smarty.MessageHub.Infrastructure.Repository;

public sealed class TelegramSessionRepository : EntityRepository<Session>, ISessionRepository
{
    public TelegramSessionRepository(IMemoryCache memoryCache, IConfiguration configuration) 
        : base(memoryCache, 
        configuration?.GetConnectionString("SessionsDb") ?? throw new ArgumentException(nameof(configuration)))
    {
    }

    public void Add(Session session)
    {
        InsertOrUpdate(session);
    }

    public bool TryGet(Guid userId, out Session? session)
    {
        session =
            GetAllOrEmpty()
            .FirstOrDefault(a=>a.UserId == userId);
        
        return session is not null;
    }
}