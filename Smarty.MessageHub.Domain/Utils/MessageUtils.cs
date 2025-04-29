using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Utils;

public static class MessageUtils
{
    public static bool TryExtractMessage<T>(MessageBase message, out T? extractedMessage) where T: MessageBase
    {
        extractedMessage = message as T;
        
        if (message is T)
        {
            return true;
        }

        if (message is MessageContainer container)
        {
            return TryExtractMessage(container.Base, out extractedMessage); 
        }
        
        return false;
    }

    public static bool TryGetSessionId(MessageBase message, out Guid extractedMessage)
    {
        bool result = TryExtractMessage<AuthenticatedMessage>(message, out var authenticatedMessage);
        extractedMessage = authenticatedMessage?.SessionId ?? default;
        return result;
    }
}