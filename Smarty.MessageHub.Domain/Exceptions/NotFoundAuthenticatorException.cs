namespace Smarty.MessageHub.Domain.Exceptions;

public class NotFoundAuthenticatorException : DomainException
{
    public NotFoundAuthenticatorException() : base("Authenticator not found")
    {
    }
}
