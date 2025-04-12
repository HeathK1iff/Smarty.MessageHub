using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Infrastructure.Handlers;

public class AddNotesMessageHandler : IMessageHandler
{
    readonly IMessagePipelineService _messagePipelineService;
    public AddNotesMessageHandler(IMessagePipelineService messagePipelineService)
    {
        _messagePipelineService = messagePipelineService;
    }

    public async Task<bool> HandleMessageAsync(MessageBase message)
    {
        if (message is not Command command)
        {
            return false;
        }

        if (string.Equals(command.CommandName, "ping", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        await _messagePipelineService.PushAsync(new Message(command)
        {
            Body = "pong"
        });

        return true;
    }
}
