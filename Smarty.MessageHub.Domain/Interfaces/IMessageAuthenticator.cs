using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IMessageAuthenticator
{
    bool IsAuthenticated(Message message, out SessionBase? sessionId);
}


