using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IMessageHandler
{
    Task<bool> HandleMessageAsync(MessageBase message);
}