using Smarty.Shared.EventBus.Abstractions.Interfaces;
using Smarty.Shared.EventBus.Interfaces;
using Smarty.TelegramGate.Domain.Events;
using Smarty.TelegramGate.Infrastructure.Handlers;

namespace Smarty.TelegramGate.Services;

public class EventBusService : BackgroundService
{
    readonly IServiceProvider _serviceProvider;
    readonly IEventBusChannelFactory _eventBusConnection;
    
    public EventBusService(IEventBusChannelFactory eventBusConnection, IServiceProvider serviceProvider)
    {
        _eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));       
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = await _eventBusConnection.CreateSubscriberAsync(stoppingToken);
        IEventHandler eventHandler = _serviceProvider.GetRequiredService<UserAddEventHandler>();
        subscriber.Subscribe(typeof(UserAddEvent), eventHandler);
    }
}