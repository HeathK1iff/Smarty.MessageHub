using Smarty.MessageHub.Application.Extensions;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Services;
using Smarty.MessageHub.Domain.Utils;
using Smarty.MessageHub.Infrastructure;
using Smarty.MessageHub.Infrastructure.Handlers;
using Smarty.MessageHub.Infrastructure.Interfaces;
using Smarty.MessageHub.Infrastructure.Repository;

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
