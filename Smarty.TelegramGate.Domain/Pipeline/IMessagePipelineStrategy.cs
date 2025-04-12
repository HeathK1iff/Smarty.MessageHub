using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Pipeline;

public interface IMessagePipelineStrategy
{
    void RegisterPipelineNodes(IMessagePipelineNodeRegistrator registrator, MessageBase message);
}
