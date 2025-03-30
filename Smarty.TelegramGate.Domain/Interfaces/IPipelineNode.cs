namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IPipelineNode<T>
{
    Task<T> PushAsync(T message);
}
