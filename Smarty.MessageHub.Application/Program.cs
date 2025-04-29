using Smarty.MessageHub.Application.Extensions;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Services;
using Smarty.MessageHub.Infrastructure;
using Smarty.MessageHub.Infrastructure.Handlers;
using Smarty.MessageHub.Infrastructure.Implementations.Service;
using Smarty.MessageHub.Infrastructure.Interfaces;
using Smarty.MessageHub.Infrastructure.Repository;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddOptions();
builder.Services.AddHostedService<TelegramService>();
  
builder.Services.AddEventBusService(() => builder.Configuration);

builder.Services.AddScoped<IClientMessageSender, ClientMessageSender>();
builder.Services.AddScoped<UserAddEventHandler>();

builder.Services.AddScoped<IMessageAuthenticator, MessageAutheticator>();
builder.Services.AddScoped<IEventMessageSender, EventBusMessageSender>();
builder.Services.AddScoped<ISessionRepository, TelegramSessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var application = builder.Build();

application.Run();
