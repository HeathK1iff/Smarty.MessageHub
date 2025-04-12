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

        public bool IsAuthenticated(MessageBase message, out Guid? userId)
        {
            userId = default;

            if (message is TelegramMessage)
            {
                userId = Guid.NewGuid();
                
                return true;
            }
            
            return false;
        }
    }
}