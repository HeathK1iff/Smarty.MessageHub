namespace Smarty.MessageHub.Domain.Interfaces;

public interface IAuthenticatorFactory
{
    IMessageAuthenticator Create(Type messageType);
}


