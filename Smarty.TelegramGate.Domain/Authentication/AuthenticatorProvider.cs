using Smarty.TelegramGate.Domain.Exceptions;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Domain.Services;

public sealed class AuthenticatorProvider : IAuthenticatorProvider
{
    readonly Dictionary<Type,IMessageAuthenticator> _authenticators = new();
    public AuthenticatorProvider(IEnumerable<IMessageAuthenticator> authenticators)
    {
        foreach (var authenticator in authenticators)
        {
            _authenticators.Add(authenticator.GetType(), authenticator);
        }
    }

    public IMessageAuthenticator Create(Type messageType)
    {
        return _authenticators.TryGetValue(messageType, out var authenticator) ? authenticator : throw new NotFoundAuthenticatorException();
    }
}


