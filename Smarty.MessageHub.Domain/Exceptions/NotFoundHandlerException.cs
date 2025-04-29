using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Domain.Exceptions;

public class NotFoundHandlerException : DomainException
{
    public NotFoundHandlerException(MessageBase message): base("Command handler not found")
    {
    }
}