using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IMessageSender
{
    Task<bool> SendAsync(MessageBase message);
}
