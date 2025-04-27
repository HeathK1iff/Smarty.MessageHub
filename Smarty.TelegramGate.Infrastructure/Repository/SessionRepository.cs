using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Smarty.TelegramGate.Infrastructure.Interfaces;

namespace Smarty.TelegramGate.Infrastructure.Repository;

public sealed class SessionRepository : EntityRepository<Session>, ISessionRepository
{
    public SessionRepository(IMemoryCache memoryCache, IConfiguration configuration) 
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
