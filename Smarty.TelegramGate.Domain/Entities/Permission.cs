namespace Smarty.TelegramGate.Domain.Repositories;

public struct Permission
{
    public string Code { get; private set; }

    public static Permission Create(string module, string action)
    {
        return new ($"{module}.{action}");
    }

    private Permission(string code)
    {
        Code = code;
    }
}
