using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Services;
using Smarty.TelegramGate.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TelegramService>();
builder.Services.AddScoped<IMessagePipelineService, MessagePipelineService>();
builder.Services.AddScoped<IMessageAuthenticator, TelegramAutheticator>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var application = builder.Build();
application.Run();