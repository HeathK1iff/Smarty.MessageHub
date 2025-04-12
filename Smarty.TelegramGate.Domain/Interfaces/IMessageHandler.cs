using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessageHandler
{
    Task<bool> HandleMessageAsync(MessageBase message);
}
