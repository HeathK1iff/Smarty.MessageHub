using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Interfaces;

public interface IClientMessageSender
{
    Task SendAsync(ClientMessage message);
}