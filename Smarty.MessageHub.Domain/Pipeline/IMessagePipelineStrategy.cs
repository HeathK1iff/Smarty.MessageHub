using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Pipeline;

public interface IMessagePipelineStrategy
{
    void RegisterPipelineNodes(IMessagePipelineNodeRegistrator registrator, MessageBase message);
}
