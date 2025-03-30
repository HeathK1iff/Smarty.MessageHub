using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces;

public interface IMessageConverter
{
    bool TryToConvert(MessageBase source, out MessageBase? target);
}
