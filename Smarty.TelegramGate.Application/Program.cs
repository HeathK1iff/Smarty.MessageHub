using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Services;
using Smarty.TelegramGate.Infrastructure;

var serviceCollection = Host.CreateApplicationBuilder(args);
serviceCollection.Services.AddHostedService<TelegramService>();
serviceCollection.Services.AddScoped<IMessagePipelineService, MessagePipelineService>();
serviceCollection.Services.AddScoped<IMessageAuthenticator, TelegramAutheticator>();

var builder = serviceCollection.Build();
builder.Run();