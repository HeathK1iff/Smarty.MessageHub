using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Domain.Pipeline.Nodes;

public sealed class InvokeMessageSendersPipelineNode : IPipelineNode
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