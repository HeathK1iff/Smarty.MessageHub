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

        public bool IsAuthenticated(MessageBase message)
        {
            if (message is TelegramMessage telegramMessage)
            {
                return true;
            }
            
            return false;
        }
    }
}