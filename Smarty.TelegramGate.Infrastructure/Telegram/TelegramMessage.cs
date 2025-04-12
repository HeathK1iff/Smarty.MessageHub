namespace Smarty.TelegramGate.Domain.Entities;

public class TelegramMessage : MessageBase
{
    public TelegramMessage(MessageBase message) : base(message)
    {
    }

    public TelegramMessage()
    {
        
    }

    public long ChatId 
    {   
        init
        {
            CustomProperties["telegram_chat_id"] = Convert.ToString(value);
        } 
    }
    public string? UserName 
    { 
        init
        {
            CustomProperties["telegram_user_name"] = value ?? string.Empty;
        }
    }
    public string? FirstName  
    { 
        init
        {
            CustomProperties["telegram_first_name"] = value ?? string.Empty;
        }
    }
    public string? LastName  
    { 
        init
        {
            CustomProperties["telegram_last_name"] = value ?? string.Empty;
        }
    }
}
