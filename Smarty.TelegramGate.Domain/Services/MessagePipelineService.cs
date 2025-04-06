using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Pipeline;

namespace Smarty.TelegramGate.Domain.Services;

public sealed class MessagePipelineService : PipelineBase<MessageBase>, IMessagePipelineService 
{
    public MessagePipelineService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Register<AuthenticationPipelineNode>();
        Register<CommandProcessPiplineNode>();
        Register<InvokeHandlersPipelineNode>();
        Register<StoreMessagePipelineNode>();
    }
}
