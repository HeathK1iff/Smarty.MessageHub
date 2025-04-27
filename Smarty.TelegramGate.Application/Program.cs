using Smarty.TelegramGate.Application.Extensions;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Services;
using Smarty.TelegramGate.Domain.Utils;
using Smarty.TelegramGate.Infrastructure;
using Smarty.TelegramGate.Infrastructure.Handlers;
using Smarty.TelegramGate.Infrastructure.Interfaces;
using Smarty.TelegramGate.Infrastructure.Repository;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddOptions();
builder.Services.AddHostedService<TelegramService>();

builder.Services.AddPipelineService();    
builder.Services.AddEventBusService(() => builder.Configuration);

builder.Services.AddScoped<ICommandParcer, CommandParcer>();
builder.Services.AddScoped<IMessageHandler, AddNotesMessageHandler>();
builder.Services.AddScoped<IMessageSender, TelegramMessageHandler>();
builder.Services.AddScoped<UserAddEventHandler>();

builder.Services.AddScoped<IMessageAuthenticator, TelegramAutheticator>();
builder.Services.AddScoped<IAuthenticatorFactory, AuthenticatorFactory>();

builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var application = builder.Build();

application.Run();
