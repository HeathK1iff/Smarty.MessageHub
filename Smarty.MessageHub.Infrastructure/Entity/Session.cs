using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Infrastructure.Interfaces;

public sealed class Session: EntityBase
{
    public Guid UserId { get; set; }
    public long SessionId { get; set; } 
}