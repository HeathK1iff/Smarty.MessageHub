namespace Smarty.MessageHub.Domain.Entities;

public abstract class Message : EntityBase
{
    public DateTime Created { get; init; }
    public required string Content { get; init; }
}
