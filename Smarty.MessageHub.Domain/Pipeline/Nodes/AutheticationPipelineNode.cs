using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Utils;

namespace Smarty.MessageHub.Domain.Pipeline.Nodes;

public sealed class AutheticationPipelineNode : IPipelineNode
{
    readonly IAuthenticatorFactory _authenticatorFactory;
    readonly IMessagePipelineService _messagePipelineService;
    public AutheticationPipelineNode(IAuthenticatorFactory authenticatorFactory,
      IMessagePipelineService messagePipelineService)
    {
        _authenticatorFactory = authenticatorFactory ?? throw new ArgumentNullException(nameof(authenticatorFactory));
        _messagePipelineService = messagePipelineService ?? throw new ArgumentNullException(nameof(messagePipelineService));
    }

    public async Task<MessageBase?> PushAsync(MessageBase message)
    {
        if (MessageUtils.TryExtractMessage<AuthenticatedMessage>(message, out _))
        {
            return message; 
        }

        var authenticator = _authenticatorFactory.Create(message.GetType());
        
        if (authenticator.IsAuthenticated(message, out Guid sessionId))
        {
            return new AuthenticatedMessage(message)
            {
                SessionId = sessionId,
            };
        }
        
        await _messagePipelineService.PushAsync(new ResponseMessage(message)
        {
            MessageData = "You are not autheticated"
        });
        
        return null;
    }
}