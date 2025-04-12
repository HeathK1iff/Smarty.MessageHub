using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Pipeline;

public interface IPipelineNode
{
    Task<MessageBase?> PushAsync(MessageBase message);
}
