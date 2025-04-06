using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Exceptions;

public class NotFoundHandlerException : DomainException
{
    public NotFoundHandlerException(MessageBase message): base("Command handler not found")
    {
    }
}