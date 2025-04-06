using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Services;

public class InvokeHandlersPipelineNode : IPipelineNode<MessageBase>
{
    readonly IEnumerable<IMessageHandler> _handlers;
    public InvokeHandlersPipelineNode(IEnumerable<IMessageHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task<MessageBase?> PushAsync(MessageBase message)
    {
        foreach (var handler in _handlers)
        {
            await handler.HandleMessageAsync(message);
        }

        return message;
    }
}
