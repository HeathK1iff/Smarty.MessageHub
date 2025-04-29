using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Entities.Messages;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.Shared.EventBus.Interfaces;

namespace Smarty.MessageHub.Domain.Services
{
    public class EventBusMessageSender : IEventMessageSender
    {
        readonly IMessageAuthenticator _messageAuthenticator;
        readonly IEventBusChannelFactory _eventBusChannelFactory;
        public EventBusMessageSender(IMessageAuthenticator messageAuthenticator,
            IEventBusChannelFactory eventBusChannelFactory)
        {
            _messageAuthenticator = messageAuthenticator;
            _eventBusChannelFactory = eventBusChannelFactory;
        }

        public async Task SendEvent(Message message, CancellationToken cancellationToken)
        {
            if (_messageAuthenticator.IsAuthenticated(message, out var session))
            {
                return;
            }
            var publisher = await _eventBusChannelFactory.CreatePublisherAsync(cancellationToken);

            await publisher.PublishAsync(new EventMessage(){
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Content = message.Content,
                Version = 1,
                UserId = session!.UserId,
            }, cancellationToken);
        }
    }
}