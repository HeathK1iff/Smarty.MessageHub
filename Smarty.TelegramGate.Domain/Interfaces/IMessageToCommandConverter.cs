using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessageToCommandConverter
{
    bool TryToConvert(MessageBase? source, out Command? target);
}
