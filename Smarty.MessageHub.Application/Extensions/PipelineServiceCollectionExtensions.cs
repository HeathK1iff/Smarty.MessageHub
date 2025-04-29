using Smarty.MessageHub.Domain.Interfaces;
using Smarty.MessageHub.Domain.Pipeline;
using Smarty.MessageHub.Domain.Pipeline.Nodes;
using Smarty.MessageHub.Domain.Services;

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