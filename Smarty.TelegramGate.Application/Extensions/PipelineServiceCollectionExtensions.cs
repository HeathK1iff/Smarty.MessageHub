using Smarty.TelegramGate.Domain.Interfaces;
using Smarty.TelegramGate.Domain.Pipeline;
using Smarty.TelegramGate.Domain.Pipeline.Nodes;
using Smarty.TelegramGate.Domain.Services;

public static class PipelineServiceCollectionExtensions
{
    public static void AddPipelineService(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IMessagePipelineService, MessagePipelineService>();
        serviceDescriptors.AddScoped<IMessagePipelineStrategy, MessagePipelineStrategy>();
        
        serviceDescriptors.AddScoped<CommandProcessPipelineNode>();
        serviceDescriptors.AddScoped<AutheticationPipelineNode>();
        serviceDescriptors.AddScoped<InvokeMessageHandlersPipelineNode>();
        serviceDescriptors.AddScoped<InvokeMessageSendersPipelineNode>();
        serviceDescriptors.AddScoped<StoreLastMessagePipelineNode>();
    }
}