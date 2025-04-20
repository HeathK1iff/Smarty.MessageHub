namespace Smarty.TelegramGate.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserLogin { get; init; }
    public UserContact[]? Contacts { get; init; }
}
