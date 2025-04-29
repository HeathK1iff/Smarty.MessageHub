using Smarty.MessageHub.Domain.Entities;
using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Infrastructure.Handlers;

public class AddNotesMessageHandler : IMessageHandler
{
    readonly IMessagePipelineService _messagePipelineService;
    
    public AddNotesMessageHandler(IMessagePipelineService messagePipelineService)
    {
        _messagePipelineService = messagePipelineService;
    }

    public async Task<bool> HandleMessageAsync(MessageBase message)
    {
        if (message is not CommandMessage command)
        {
            return false;
        }

        if (!string.Equals(command.Name, "ping", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        await _messagePipelineService.PushAsync(new ResponseMessage(message)
        {
            MessageData = "pong"
        });

        return true;
    }
}
