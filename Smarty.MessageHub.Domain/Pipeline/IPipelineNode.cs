using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Pipeline;

public interface IPipelineNode
{
    Task<MessageBase?> PushAsync(MessageBase message);
}
