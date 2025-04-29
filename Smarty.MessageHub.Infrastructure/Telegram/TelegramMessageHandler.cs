using Microsoft.Extensions.Configuration;
using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Exceptions;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Utils;
using Telegram.Bot;

namespace Smarty.MessageHub.Infrastructure;

public class TelegramMessageHandler : IMessageSender
{
    readonly IConfiguration _configuration;

    public TelegramMessageHandler(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<bool> SendAsync(MessageBase message)
    {
        if (!TryExtractTelegramMessage(message, out var telegramMessage))
        {
            return false;
        }

        var telegramConnectionString = _configuration.GetConnectionString("Telegram");

        if (telegramConnectionString is null)
        {
            throw new InvalidConfigurationException("Telegram client is not configured");
        }


        var _client = new TelegramBotClient(telegramConnectionString);

        if (string.IsNullOrWhiteSpace(telegramMessage!.MessageData))
        {
            return false;
        }

        await _client.SendMessage(telegramMessage!.ChatId, telegramMessage!.MessageData);

        return true;
    }

    protected bool TryExtractTelegramMessage(MessageBase messageBase, out TelegramMessage? message)
    {
        message = default;

        if (messageBase is TelegramMessage telegramMessage)
        {
            message = telegramMessage;
            return true;
        }

        if (MessageUtils.TryExtractMessage<TelegramMessage>(messageBase, out var sourceMessage) &&
            sourceMessage is not null &&
            messageBase is IMessageData bodyMessage)
        {
            message = new TelegramMessage()
            {
                ChatId = sourceMessage.ChatId,
                MessageData = bodyMessage.MessageData
            };

            return true;
        }

        return false;
    }
}
