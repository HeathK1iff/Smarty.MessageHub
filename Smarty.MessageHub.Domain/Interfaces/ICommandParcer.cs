namespace Smarty.MessageHub.Domain.Interfaces;

public interface ICommandParcer
{
    public bool TryToParce(string body, out (string CommandName, string[] CommandParams) comamnd);
}
