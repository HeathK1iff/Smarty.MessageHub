using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Infrastructure.Interfaces;

public sealed class Session: SessionBase
{
    public long ChatId { get; set; } 
}