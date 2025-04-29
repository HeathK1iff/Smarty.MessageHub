using Smarty.Shared.EventBus.Interfaces;
using Smarty.MessageHub.Domain.Events;
using Smarty.MessageHub.Infrastructure.Handlers;
using Microsoft.Extensions.Hosting;

namespace Smarty.MessageHub.Infrastructure.Implementations.Service;

public sealed class EventBusService : BackgroundService
{
    readonly IEventBusChannelFactory _eventBusConnection;
    
    public EventBusService(IEventBusChannelFactory eventBusConnection, IServiceProvider serviceProvider)
    {
        _eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = await _eventBusConnection.CreateSubscriberAsync(stoppingToken);
        await subscriber.Subscribe(typeof(UserAddEvent), typeof(UserAddEventHandler));
    }
}