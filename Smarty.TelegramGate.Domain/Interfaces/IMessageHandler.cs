using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Services;

public interface IMessageHandler
{
    Task<bool> HandleMessageAsync(MessageBase message);
}
