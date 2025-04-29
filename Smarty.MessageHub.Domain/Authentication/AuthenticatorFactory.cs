using Smarty.MessageHub.Domain.Exceptions;
using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Domain.Services;

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


