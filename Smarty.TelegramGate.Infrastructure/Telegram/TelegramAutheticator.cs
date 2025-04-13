using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Infrastructure
{
    public class TelegramAutheticator : IMessageAuthenticator
    {
        public Type GetMessageType()
        {
            return typeof(TelegramMessage);
        }

        public bool IsAuthenticated(MessageBase message, out Guid sessionId)
        {
            sessionId = default;

            if (message is TelegramMessage)
            {
                sessionId = Guid.NewGuid();
                return true;
            }
            
            return false;
        }
    }
}