using Microsoft.Extensions.Configuration;
using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Exceptions;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Infrastructure.Interfaces;
using Telegram.Bot;

namespace Smarty.MessageHub.Infrastructure;

public class ClientMessageSender: IClientMessageSender
{
    readonly IConfiguration _configuration;
    ISessionRepository _sessionRepository;

    public ClientMessageSender(IConfiguration configuration, ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task SendAsync(ClientMessage message)
    {
        var connectionString = _configuration.GetConnectionString("Telegram");

        if (connectionString is null)
        {
            throw new InvalidConfigurationException("Telegram client is not configured");
        }


        var _client = new TelegramBotClient(connectionString);

        if (string.IsNullOrWhiteSpace(message.Content))
        {
            return;
        }

        if (_sessionRepository.TryGet(message.UserId, out Session? session))
        {
             await _client.SendMessage(session!.ChatId, message.Content);
        }

        return;
    }
}
