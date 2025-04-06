using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessageAuthenticator
{
    Type GetMessageType();
    bool IsAuthenticated(MessageBase message);
}


