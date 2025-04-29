using Smarty.Shared.EventBus.Abstractions.Events;

namespace Smarty.MessageHub.Domain.Events;

public sealed class UserAddEvent : EventBase
{
    public Guid CacheObjectId { get; init; }
}
