using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Interfaces.Converters;
using Smarty.TelegramGate.Domain.Pipeline;
using Smarty.TelegramGate.Domain.Pipeline.Nodes;
using Smarty.TelegramGate.Domain.Services;
using Smarty.TelegramGate.Infrastructure;
using Smarty.TelegramGate.Infrastructure.Handlers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddHostedService<TelegramService>();
builder.Services.AddScoped<IMessagePipelineService, MessagePipelineService>();
builder.Services.AddScoped<IMessagePipelineStrategy, MessagePipelineStrategy>();
builder.Services.AddTransient<CommandProcessPipelineNode>();
builder.Services.AddTransient<AuthenticationPipelineNode>();
builder.Services.AddTransient<InvokeMessageHandlersPipelineNode>();
builder.Services.AddTransient<StoreLastMessagePipelineNode>();
builder.Services.AddTransient<IMessageToCommandConverter, MessageToCommandConverter>();
builder.Services.AddTransient<IMessageHandler, AddNotesMessageHandler>();
builder.Services.AddTransient<IMessageHandler, TelegramMessageHandler>();

builder.Services.AddScoped<IMessageAuthenticator, TelegramAutheticator>();
builder.Services.AddTransient<IAuthenticatorProvider, AuthenticatorProvider>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var application = builder.Build();


application.Run();
