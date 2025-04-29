using Microsoft.Extensions.Caching.Memory;
using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Utils;

namespace Smarty.MessageHub.Domain.Pipeline.Nodes;

public sealed class CommandProcessPipelineNode : IPipelineNode
{
    readonly ICommandParcer _commandParcer;
    readonly IMemoryCache _memoryCache;

    public CommandProcessPipelineNode(ICommandParcer commandParcer, 
        IMemoryCache memoryCache)
    {
        _commandParcer = commandParcer ?? throw new ArgumentNullException(nameof(commandParcer));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task<MessageBase?> PushAsync(MessageBase message)
    {
        if (!MessageUtils.TryExtractMessage<Message>(message, out var msg))
        {
            return Task.FromResult<MessageBase?>(message); 
        }

        if (_commandParcer.TryToParce(msg?.MessageData ?? string.Empty, out var command) )
        {
            if (!MessageUtils.TryGetSessionId(message, out var sessionId))
            {
                return Task.FromResult<MessageBase?>(message); 
            }

            var newMessage = new CommandMessage(message)
            {
                Name =  command.CommandName,
                Params = command.CommandParams,
                Previous = _memoryCache.TryGetValue<MessageBase>(sessionId, out var value) ? value : default
            };
            
            return Task.FromResult<MessageBase?>(newMessage);
        }
        
        return Task.FromResult<MessageBase?>(message);
    }
}