using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Pipeline;

namespace Smarty.TelegramGate.Domain.Services;

public class MessagePipelineService : IMessagePipelineService, IMessagePipelineNodeRegistrator 
{
    readonly LinkedList<Type> s_nodes = new();
    readonly IServiceProvider _serviceProvider;
    readonly IMessagePipelineStrategy _messagePipelineStrategy;

    public MessagePipelineService(IServiceProvider serviceProvider, IMessagePipelineStrategy messagePipelineStrategy)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _messagePipelineStrategy = messagePipelineStrategy ?? throw new ArgumentNullException(nameof(messagePipelineStrategy));
    } 

    public void RegisterNode<T>() where T : IPipelineNode
    {
        s_nodes.AddLast(typeof(T));
    }
    
    public async Task<MessageBase?> PushAsync(MessageBase message)
    {
        MessageBase? pipelineMessage = message; 
        
        s_nodes.Clear();
        _messagePipelineStrategy.RegisterPipelineNodes(this, pipelineMessage);
        var node = s_nodes.First;
        
        while ((node != null) && (pipelineMessage != null))
        {
            var service = _serviceProvider.GetService(node.Value);

            if (service is IPipelineNode pipelineNode)
            {                
                pipelineMessage = await pipelineNode.PushAsync(pipelineMessage);
            }
            else
            {
                throw new InvalidDataException();
            }

            node = node.Next;
        }

        return pipelineMessage;
    }
}
