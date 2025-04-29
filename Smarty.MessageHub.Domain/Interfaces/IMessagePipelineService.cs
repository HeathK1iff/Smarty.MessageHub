using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IMessagePipelineService
{
    Task<MessageBase?> PushAsync(MessageBase message);
}
