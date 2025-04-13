namespace Smarty.TelegramGate.Domain.Entities
{
    public sealed class AuthenticatedMessage : MessageContainer
    {
        public AuthenticatedMessage(MessageBase @base) : base(@base)
        {
        }

        public Guid SessionId { get; init; }
    }
}