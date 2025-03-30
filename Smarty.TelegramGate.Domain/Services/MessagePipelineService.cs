using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Pipeline;

namespace Smarty.TelegramGate.Domain.Services;

public class MessagePipelineService : PipelineBase<MessageBase>, IMessagePipelineService 
{
    public MessagePipelineService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Register<MessageAuthenticationPipelineNode>();
        Register<CommandProcessPiplineNode>();
        Register<MessageHandlersPipelineNode>();
    }
}
