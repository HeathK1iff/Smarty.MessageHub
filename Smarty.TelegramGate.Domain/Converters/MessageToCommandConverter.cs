using System.Text.RegularExpressions;
using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces.Converters;

public class MessageToCommandConverter : IMessageConverter
{
    public bool TryToConvert(MessageBase source, out MessageBase? target)
    {
        target = default;

        if (source is not Message message)
        {
            return false;
        }

        string body = message.Body?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(body))
        {
            return false;
        }


        var match = Regex.Match(body, @"^\/(\w+)\s(.+)$");

        if (!match.Success)
        {
            return false;
        }

        target = new Command() 
        {
            CommandName = match.Groups[1].Value,
            Params = match.Groups[2].Value.Split(' ')
        };

        return true;
    }
}