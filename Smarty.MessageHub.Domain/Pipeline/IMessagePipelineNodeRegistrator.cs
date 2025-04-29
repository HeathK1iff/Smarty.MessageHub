namespace Smarty.MessageHub.Domain.Pipeline;

public interface IMessagePipelineNodeRegistrator
{
    void RegisterNode<T>() where T : IPipelineNode;
}
