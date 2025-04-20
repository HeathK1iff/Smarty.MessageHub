namespace Smarty.TelegramGate.Domain.Entities;

public sealed class UserContact
{
    public ContactType Type { get; init; }
    public string? ContactInformation { get; init; }
}
