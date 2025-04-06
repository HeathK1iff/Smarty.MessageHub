using Microsoft.Extensions.Caching.Memory;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public class CommandProcessPiplineNode : IPipelineNode<MessageBase>
{
    readonly IMessageToCommandConverter _messageConverter;
    readonly IMemoryCache _memoryCache;
    public CommandProcessPiplineNode(IMessageToCommandConverter messageConverter, 
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
            commandMessage.Previous = _memoryCache.TryGetValue<Message>(message.UserId, out var value) ? value : default;

            return Task.FromResult<MessageBase?>(commandMessage);
        }
        
        return Task.FromResult<MessageBase?>(message);
    }
}