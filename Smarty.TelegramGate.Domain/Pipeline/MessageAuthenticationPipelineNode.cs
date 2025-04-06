using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline;

public sealed class MessageAuthenticationPipelineNode : IPipelineNode<MessageBase>
{
    readonly IAuthenticatorProvider _authenticatorsService;
    public MessageAuthenticationPipelineNode(IAuthenticatorProvider authenticatorsService)
    {
        _authenticatorsService = authenticatorsService ?? throw new ArgumentNullException(nameof(authenticatorsService));
    }

    public Task<MessageBase?> PushAsync(MessageBase message)
    {   
        var authenticator =_authenticatorsService.Create(message.GetType());
        if(authenticator.IsAuthenticated(message))
        {
            return Task.FromResult(message)!;
        }

        throw new AccessViolationException();
    }
}