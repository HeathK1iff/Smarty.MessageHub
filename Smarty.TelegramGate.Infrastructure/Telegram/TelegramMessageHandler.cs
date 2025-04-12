using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;
using Telegram.Bot;

namespace Smarty.TelegramGate.Infrastructure;

public class TelegramMessageHandler : IMessageHandler
{
    readonly TelegramBotClient _client;
    
    public TelegramMessageHandler()
    {
        _client = new TelegramBotClient("");
    }
    
    public Task<bool> HandleMessageAsync(MessageBase message)
    {
        if (message is not Message)
        {
            return Task.FromResult(false);
        }

        if (message.CustomProperties.TryGetValue("telegram_chat_id", out var sChatId) ?
           long.TryParse(sChatId, out long chatId) : false)
        {        
            _client.SendMessage(chatId, message.Body); 
        }

        return Task.FromResult(true);
    }
}
