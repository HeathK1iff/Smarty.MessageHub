namespace Smarty.TelegramGate.Domain.Pipeline;

public interface IMessagePipelineNodeRegistrator
{
    void RegisterNode<T>() where T : IPipelineNode;
}
