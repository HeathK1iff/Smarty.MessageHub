using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smarty.TelegramGate.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
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

        private async Task OnMessage(Message msg, UpdateType type)
        {
            using var scope =  _serviceProvider.CreateScope();

            var pipelineService = scope.ServiceProvider.GetRequiredService<IMessagePipelineService>();

            await pipelineService.PushAsync(new TelegramMessage(msg){
                Body = msg.Text
            });            
        }
    }
}