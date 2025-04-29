using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Exceptions;
using Smarty.MessageHub.Domain.Services;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Smarty.MessageHub.Infrastructure.Implementations.Service;

public sealed class TelegramService : BackgroundService
{
    TelegramBotClient? _client;
    readonly IServiceProvider _serviceProvider;
    readonly IConfiguration _configuration;

    public TelegramService(IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var token = _configuration.GetConnectionString("Telegram") ?? string.Empty;

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidConfigurationException();
        }
        
        _client = new TelegramBotClient(token, cancellationToken: stoppingToken);
        _client.OnMessage += async (Telegram.Bot.Types.Message msg, UpdateType type) => 
        {
            using var scope = _serviceProvider.CreateScope();

            var eventMessageSenderService = scope.ServiceProvider.GetRequiredService<IEventMessageSender>();
            
            await eventMessageSenderService.SendEvent(new TelegramMessage()
            {
                Id = Guid.NewGuid(),
                ChatId = msg.Chat.Id,
                FirstName = msg.Chat.FirstName,
                LastName = msg.Chat.LastName,
                UserName = msg.Chat.Username,
                Content = msg?.Text ?? string.Empty
            }, stoppingToken);
        };

        return Task.CompletedTask;
    }
}

