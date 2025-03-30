using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Services;

public class MessageHandlersPipelineNode : IPipelineNode<MessageBase>
{
    readonly IEnumerable<IMessageHandler> _handlers;
    public MessageHandlersPipelineNode(IEnumerable<IMessageHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task<MessageBase> PushAsync(MessageBase message)
    {
        foreach (var handler in _handlers)
        {
            if (!await handler.HandleMessageAsync(message))
            {
                break;
            } 
        }

        return message;
    }
}
