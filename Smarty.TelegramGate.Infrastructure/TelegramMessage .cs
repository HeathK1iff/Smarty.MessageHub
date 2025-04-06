using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Infrastructure;

public class TelegramMessage : Message
{
    public TelegramMessage(Telegram.Bot.Types.Message message)
    {
        SessionId = message.Chat.Id;
        UserName = message.Chat.Username ?? string.Empty;
        FirstName = message.Chat.FirstName ?? string.Empty;
        LastName = message.Chat.LastName ?? string.Empty;
    }

    public long SessionId { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

}
