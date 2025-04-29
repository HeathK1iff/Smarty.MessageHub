using Smarty.Shared.EventBus;
using Smarty.Shared.EventBus.Interfaces;
using Smarty.Shared.EventBus.Options;
using Smarty.MessageHub.Infrastructure.EventBus;

namespace Smarty.MessageHub.Application.Extensions
{
    public static class EventBusServiceCollectionExtensions
    {
        public static void AddEventBusService(this IServiceCollection serviceDescriptors, Func<IConfiguration> config)
        {
            serviceDescriptors.AddSingleton<IEventBusChannelFactory, EventBusChannelFactory>();
            serviceDescriptors.AddSingleton<IEventQueueResolver, EventQueueResolver>();
            serviceDescriptors.Configure<EventBusOptions>(config.Invoke().GetSection("EventBus")); 
            serviceDescriptors.AddSingleton<IEventSerializator, EventSerializator>();
        }
    }
}