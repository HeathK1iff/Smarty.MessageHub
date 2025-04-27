namespace Smarty.TelegramGate.Infrastructure.Interfaces;

public interface ISessionRepository
{
    void Add(Session session);
    bool TryGet(Guid userId, out Session? session);
}
