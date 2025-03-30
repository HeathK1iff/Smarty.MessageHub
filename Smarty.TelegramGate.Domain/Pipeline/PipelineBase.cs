using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public abstract class PipelineBase<T>: IPipelineNode<T>
{
    readonly LinkedList<Type> s_nodes = new();
    readonly IServiceProvider _serviceProvider;

    public PipelineBase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    public async Task<T> PushAsync(T message)
    {
        var node = s_nodes.First;
        T? piplineMessage = message; 

        while ((node != null) && (piplineMessage != null))
        {
            var service = _serviceProvider.GetService(node.Value);

            if (service is IPipelineNode<T> pipelineNode)
            {
                piplineMessage = await pipelineNode.PushAsync(piplineMessage);
            }

            node = node.Next;
        }

        return piplineMessage;
    }

    protected void Register<K>() where K : IPipelineNode<T>
    {
        s_nodes.AddLast(typeof(K));
    }
}
