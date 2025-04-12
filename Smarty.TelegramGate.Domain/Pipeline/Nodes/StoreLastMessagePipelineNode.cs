
using Microsoft.Extensions.Caching.Memory;
using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Pipeline.Nodes;

public sealed class StoreLastMessagePipelineNode : IPipelineNode
{
    readonly IMemoryCache _memoryCache;
    public StoreLastMessagePipelineNode(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task<MessageBase?> PushAsync(MessageBase message)
    {
        if (message is Command)
        {
            return Task.FromResult<MessageBase?>(message);
        }

        if (message.UserId.HasValue)
        {
            _memoryCache.Set(message.UserId.Value, message);
        }
        
        return Task.FromResult(message)!;
    }
}
