using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Smarty.TelegramGate.Infrastructure
{
    public class TelegramService: BackgroundService
    {
        TelegramBotClient? _client;
        readonly IServiceProvider _serviceProvider;
        readonly IConfiguration _configuration;

        public TelegramService(IConfiguration configuration, 
            IServiceProvider serviceProvider)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProvider =  serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client = new TelegramBotClient("", cancellationToken: stoppingToken);
            _client.OnMessage += new TelegramBotClient.OnMessageHandler(OnMessage);
            
            return Task.CompletedTask;
        }

        private async Task OnMessage(Telegram.Bot.Types.Message msg, UpdateType type)
        {
            using var scope =  _serviceProvider.CreateScope();

            var pipelineService = scope.ServiceProvider.GetRequiredService<IMessagePipelineService>();

            await pipelineService.PushAsync(new TelegramMessage(){
                ChatId = msg.Chat.Id,
                FirstName = msg.Chat.FirstName,
                LastName = msg.Chat.LastName,
                UserName = msg.Chat.Username,
                Body = msg.Text
            });            
        }
    }
}