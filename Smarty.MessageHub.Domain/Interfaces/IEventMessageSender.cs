using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Services
{
    public interface IEventMessageSender
    {
        Task SendEvent(Message message, CancellationToken cancellationToken);
    }
}