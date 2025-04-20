using Smarty.Shared.EventBus.Abstractions.Events;

namespace Smarty.TelegramGate.Domain.Events;

public sealed class UserAddEvent : EventBase
{
    public Guid CacheObjectId { get; init; }
}
