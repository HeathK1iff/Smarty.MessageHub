namespace Smarty.MessageHub.Domain.Entities;

public class TelegramMessage : Message
{
    public long ChatId { get; init; }
    public string? UserName { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
