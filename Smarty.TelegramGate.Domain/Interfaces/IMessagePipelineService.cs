using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessagePipelineService
{
    Task<MessageBase?> PushAsync(MessageBase message);
}
