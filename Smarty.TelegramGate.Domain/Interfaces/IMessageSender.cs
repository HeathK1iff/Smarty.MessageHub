using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessageSender
{
    Task<bool> SendAsync(MessageBase message);
}
