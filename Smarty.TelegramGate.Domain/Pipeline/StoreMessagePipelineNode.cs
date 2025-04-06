
using Microsoft.Extensions.Caching.Memory;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public class StoreMessagePipelineNode : IPipelineNode<MessageBase>
{
    readonly IMemoryCache _memoryCache;
    public StoreMessagePipelineNode(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task<MessageBase?> PushAsync(MessageBase message)
    {
        if (message is Command)
        {
            return Task.FromResult<MessageBase?>(message);
        }

        _memoryCache.Set(message.UserId, message);

        return Task.FromResult(message)!;
    }
}
