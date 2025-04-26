using Smarty.Shared.EventBus;
using Smarty.Shared.EventBus.Interfaces;
using Smarty.Shared.EventBus.Options;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Pipeline;
using Smarty.TelegramGate.Domain.Pipeline.Nodes;
using Smarty.TelegramGate.Domain.Services;
using Smarty.TelegramGate.Domain.Utils;
using Smarty.TelegramGate.Infrastructure;
using Smarty.TelegramGate.Infrastructure.EventBus;
using Smarty.TelegramGate.Infrastructure.Handlers;
using Smarty.TelegramGate.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddOptions();
builder.Services.AddHostedService<TelegramService>();
builder.Services.AddScoped<IMessagePipelineService, MessagePipelineService>();
builder.Services.AddScoped<IMessagePipelineStrategy, MessagePipelineStrategy>();
builder.Services.AddTransient<CommandProcessPipelineNode>();
builder.Services.AddTransient<AutheticationPipelineNode>();
builder.Services.AddTransient<InvokeMessageHandlersPipelineNode>();
builder.Services.AddTransient<InvokeMessageSendersPipelineNode>();
builder.Services.AddTransient<StoreLastMessagePipelineNode>();
builder.Services.AddTransient<ICommandParcer, CommandParcer>();
builder.Services.AddTransient<IMessageHandler, AddNotesMessageHandler>();
builder.Services.AddTransient<IMessageSender, TelegramMessageHandler>();
builder.Services.AddTransient<IEventSerializator, EventSerializator>();
builder.Services.AddTransient<UserAddEventHandler>();
builder.Services.AddTransient<IUsersRepository, UserRepository>();
builder.Services.AddHostedService<EventBusService>();
builder.Services.AddTransient<IEventBusChannelFactory, EventBusChannelFactory>();
builder.Services.AddTransient<IEventQueueResolver, EventQueueResolver>();
builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBus"));
    

builder.Services.AddScoped<IMessageAuthenticator, TelegramAutheticator>();
builder.Services.AddTransient<IAuthenticatorFactory, AuthenticatorFactory>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var application = builder.Build();

application.Run();