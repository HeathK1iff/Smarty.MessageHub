using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Infrastructure.Interfaces;

namespace Smarty.MessageHub.Infrastructure;

public class TelegramAutheticator : IMessageAuthenticator
{
    readonly IUserRepository _userRepository;
    readonly ISessionRepository _sessionRepository;
    public TelegramAutheticator(IUserRepository userRepository, ISessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
    }

    public Type GetMessageType()
    {
        return typeof(TelegramMessage);
    }

    public bool IsAuthenticated(MessageBase message, out Guid sessionId)
    {
        sessionId = default;

        if (message is TelegramMessage telegramMessage)
        {
            var users = _userRepository.FindByContactInformation(telegramMessage.UserName ?? string.Empty);

            if (users is { Length: > 0 })
            {
                var user = users.First();

                if (!_sessionRepository.TryGet(user.Id, out var session))
                {
                    sessionId = Guid.NewGuid();

                    _sessionRepository.Add(new Session()
                    {
                        Id = sessionId,
                        SessionId = telegramMessage.ChatId,
                        UserId = user.Id
                    });

                    return true;
                }

                sessionId = session?.Id ?? Guid.Empty;
                return true;
            }
        }

        return false;
    }
}
