using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public class MessageAuthenticationPipelineNode : IPipelineNode<MessageBase>
{
    public Task<MessageBase> PushAsync(MessageBase message)
    {
        return Task.FromResult(message);
    }
}
