using System.Text.RegularExpressions;
using Smarty.MessageHub.Domain.Interfaces;

namespace Smarty.MessageHub.Domain.Utils;

public class CommandParcer : ICommandParcer
{
    public bool TryToParce(string body, out (string CommandName, string[] CommandParams) command)
    {
        command = default;

        if (string.IsNullOrWhiteSpace(body))
        {
            return false;
        }

        var match = Regex.Match(body.Trim(), @"^\#(\w+)(\s(.+))?$");

        if (!match.Success)
        {
            return false;
        }

        command = (
            CommandName: match.Groups[1].Value,
            CommandParams: match.Groups[2].Value.Trim().Split(' ')
        );

        return true;
    }
}