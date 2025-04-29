using Smarty.Shared.EventBus.Abstractions.Events;

namespace Smarty.MessageHub.Domain.Entities.Messages;

public sealed class EventMessage : EventBase
{
    public required string Content { get; init; }
}
