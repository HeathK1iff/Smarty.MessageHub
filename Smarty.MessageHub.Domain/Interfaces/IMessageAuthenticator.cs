using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IMessageAuthenticator
{
    Type GetMessageType();
    bool IsAuthenticated(MessageBase message, out Guid sessionId);
}


