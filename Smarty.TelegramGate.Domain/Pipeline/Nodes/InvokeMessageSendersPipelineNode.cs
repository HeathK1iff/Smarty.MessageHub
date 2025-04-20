using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline.Nodes;

public class InvokeMessageSendersPipelineNode : IPipelineNode
{
    readonly IEnumerable<IMessageSender> _messageSenders;
    public InvokeMessageSendersPipelineNode(IEnumerable<IMessageSender> messageSenders)
    {
        _messageSenders = messageSenders;
    }

    public async Task<MessageBase?> PushAsync(MessageBase message)
    {
        bool handled = false;
        foreach (var messageSender in _messageSenders)
        {
            handled |= await messageSender.SendAsync(message);
        }

        return message;
    }
}