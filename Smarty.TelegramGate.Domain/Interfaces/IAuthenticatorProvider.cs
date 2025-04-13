namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IAuthenticatorFactory
{
    IMessageAuthenticator Create(Type messageType);
}


