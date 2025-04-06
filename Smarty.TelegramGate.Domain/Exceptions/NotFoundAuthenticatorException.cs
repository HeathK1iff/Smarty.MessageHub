namespace Smarty.TelegramGate.Domain.Exceptions;

public class NotFoundAuthenticatorException : DomainException
{
    public NotFoundAuthenticatorException() : base("Authenticator not found")
    {
    }
}
