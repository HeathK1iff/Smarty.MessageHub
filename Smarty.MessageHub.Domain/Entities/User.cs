namespace Smarty.MessageHub.Domain.Entities;

public sealed class User : EntityBase
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserLogin { get; init; }
    public UserContact[]? Contacts { get; init; }

    public override object Clone()
    {
        return new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            Id = Id,
            UserLogin = UserLogin,
            Contacts = Contacts.Select(a => a.Clone() as UserContact).ToArray()
        };
    }
}
