using Microsoft.Extensions.Configuration;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Exceptions;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Utils;
using Telegram.Bot;

namespace Smarty.TelegramGate.Infrastructure;

public class TelegramMessageHandler : IMessageHandler
{
    readonly IConfiguration _configuration;
    
    public TelegramMessageHandler(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
    }
    
    public Task<bool> HandleMessageAsync(MessageBase message)
    {
        if (message is not Message)
        {
            return Task.FromResult(false);
        }
        
        if (message is ResponseMessage responseMessage &&
            MessageUtils.TryExtractMessage<TelegramMessage>(responseMessage, out var telegramMessage))
        {
            var telegramConnectionString = _configuration.GetConnectionString("Telegram");
            
            if (telegramConnectionString is null)
            {
                throw new InvalidConfigurationException("Telegram client is not configured");
            }

            if (telegramMessage is not null)
            {
                var _client = new TelegramBotClient(telegramConnectionString);
                _client.SendMessage(telegramMessage.ChatId, responseMessage.Message);
            }
        }

        return Task.FromResult(true);
    }
}
