using Microsoft.Extensions.Caching.Memory;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public class CommandProcessPiplineNode : IPipelineNode<MessageBase>
{
    readonly IMessageConverter _messageConverter;
    readonly IMemoryCache _memoryCache;
    public CommandProcessPiplineNode(IMessageConverter messageConverter, 
        IMemoryCache memoryCache)
    {
        _messageConverter = messageConverter ?? throw new ArgumentNullException(nameof(messageConverter));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task<MessageBase> PushAsync(MessageBase message)
    {
        if (_messageConverter.TryToConvert(message, out var convertedMessage) && 
            (convertedMessage is not null))
        {
            return Task.FromResult(convertedMessage);
        }

        _memoryCache.Set(message.SessionId, message, new MemoryCacheEntryOptions() {
            SlidingExpiration = TimeSpan.FromSeconds(10)
        });
        
        return Task.FromResult(message);
    }
}