namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IAuthenticatorProvider
{
    IMessageAuthenticator Create(Type messageType);
}


