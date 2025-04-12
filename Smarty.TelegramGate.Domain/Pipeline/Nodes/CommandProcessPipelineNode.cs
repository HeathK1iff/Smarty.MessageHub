using Microsoft.Extensions.Caching.Memory;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline.Nodes;

public sealed class CommandProcessPipelineNode : IPipelineNode
{
    readonly IMessageToCommandConverter _messageConverter;
    readonly IMemoryCache _memoryCache;

    public CommandProcessPipelineNode(IMessageToCommandConverter messageConverter, 
        IMemoryCache memoryCache)
    {
        _messageConverter = messageConverter ?? throw new ArgumentNullException(nameof(messageConverter));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task<MessageBase?> PushAsync(MessageBase message)
    {
        if (_messageConverter.TryToConvert(message, out var commandMessage) && 
            (commandMessage is not null))
        {
            if (message.UserId.HasValue)
            {
                commandMessage.Previous = _memoryCache.TryGetValue<MessageBase>(message.UserId.Value, out var value) ? value : default;
            }
            
            return Task.FromResult<MessageBase?>(commandMessage);
        }
        
        return Task.FromResult<MessageBase?>(message);
    }
}