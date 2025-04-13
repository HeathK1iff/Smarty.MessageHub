using Smarty.TelegramGate.Domain.Exceptions;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Services;

public sealed class AuthenticatorFactory : IAuthenticatorFactory
{
    readonly Dictionary<Type,IMessageAuthenticator> _authenticators = new();
    public AuthenticatorFactory(IEnumerable<IMessageAuthenticator> authenticators)
    {
        foreach (var authenticator in authenticators)
        {
            _authenticators.Add(authenticator.GetMessageType(), authenticator);
        }
    }

    public IMessageAuthenticator Create(Type messageType)
    {
        return _authenticators.TryGetValue(messageType, out var authenticator) ? authenticator : throw new NotFoundAuthenticatorException();
    }
}


