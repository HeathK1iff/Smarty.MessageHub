using System.Text.RegularExpressions;
using Smarty.TelegramGate.Domain.Entities;

namespace Smarty.TelegramGate.Domain.Interfaces.Converters;

public class MessageToCommandConverter : IMessageToCommandConverter
{
    public bool TryToConvert(MessageBase? source, out Command? target)
    {
        target = default;

        string body = source?.Body?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(body))
        {
            return false;
        }


        var match = Regex.Match(body, @"^\#(\w+)(\s(.+))?$");

        if (!match.Success)
        {
            return false;
        }

        target = new Command(source) 
        {
            CommandName = match.Groups[1].Value,
            Params = match.Groups[2].Value.Split(' ')
        };

        return true;
    }
}