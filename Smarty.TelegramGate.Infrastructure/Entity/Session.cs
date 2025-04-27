using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Infrastructure.Interfaces;

public sealed class Session: EntityBase
{
    public Guid UserId { get; set; }
    public long SessionId { get; set; } 
}