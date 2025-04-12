using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Pipeline.Nodes;

public sealed class AuthenticationPipelineNode : IPipelineNode
{
    readonly IAuthenticatorProvider _authenticatorsService;
    readonly IMessagePipelineService _messagePipelineService;
    public AuthenticationPipelineNode(IAuthenticatorProvider authenticatorsService,
      IMessagePipelineService messagePipelineService)
    {
        _authenticatorsService = authenticatorsService ?? throw new ArgumentNullException(nameof(authenticatorsService));
        _messagePipelineService = messagePipelineService;
    }

    public async Task<MessageBase?> PushAsync(MessageBase message)
    {
        var authenticator = _authenticatorsService.Create(message.GetType());
        if (authenticator.IsAuthenticated(message, out var userId))
        {
            if (userId.HasValue)
            {
                (message as IUserIdAssigner).SetUserId(userId.Value);
            }
            
            return message;
        }
        
        await _messagePipelineService.PushAsync(new Message(message){
                Body = "You are not autheticated"
            });
        
        return null;
    }
}