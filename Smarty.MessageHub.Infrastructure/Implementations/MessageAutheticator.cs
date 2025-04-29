using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Infrastructure.Interfaces;

namespace Smarty.MessageHub.Infrastructure;

public class MessageAutheticator : IMessageAuthenticator
{
    readonly IUserRepository _userRepository;
    readonly ISessionRepository _sessionRepository;
    public MessageAutheticator(IUserRepository userRepository, ISessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
    }

    public Type GetMessageType()
    {
        return typeof(TelegramMessage);
    }

    public bool IsAuthenticated(Message message, out SessionBase? session)
    {
        session = default;

        if (message is TelegramMessage telegramMessage)
        {
            var users = _userRepository.FindByContactInformation(telegramMessage.UserName ?? string.Empty);

            if (users is { Length: > 0 })
            {
                var user = users.First();

                if (!_sessionRepository.TryGet(user.Id, out var sessionEntity))
                {
                    var newSession = new Session()
                    {
                        Id = Guid.NewGuid(),
                        ChatId = telegramMessage.ChatId,
                        UserId = user.Id
                    };
                    
                    _sessionRepository.Add(newSession);
                    session = newSession;

                    return true;
                }

                session = sessionEntity;

                return true;
            }
        }

        return false;
    }
}
