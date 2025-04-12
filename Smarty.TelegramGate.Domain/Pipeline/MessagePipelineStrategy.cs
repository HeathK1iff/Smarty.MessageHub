using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Pipeline.Nodes;

namespace Smarty.TelegramGate.Domain.Pipeline;

public class MessagePipelineStrategy : IMessagePipelineStrategy
{
    public void RegisterPipelineNodes(IMessagePipelineNodeRegistrator registrator, MessageBase message)
    {
        if (message is not Message)
        {
            registrator.RegisterNode<AuthenticationPipelineNode>();
            registrator.RegisterNode<CommandProcessPipelineNode>();
            registrator.RegisterNode<InvokeMessageHandlersPipelineNode>();
            registrator.RegisterNode<StoreLastMessagePipelineNode>();
        }
        else
        {
            registrator.RegisterNode<InvokeMessageHandlersPipelineNode>();
        }
    }
        
}
