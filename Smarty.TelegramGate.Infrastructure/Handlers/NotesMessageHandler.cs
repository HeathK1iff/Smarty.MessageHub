using System.Transactions;
using Microsoft.Extensions.Caching.Distributed;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Services;

namespace Smarty.TelegramGate.Infrastructure.Handlers;

public class AddNotesMessageHandler : IMessageHandler
{
    readonly IDistributedCache _distributedCache;
    public AddNotesMessageHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    public Task<bool> HandleMessageAsync(MessageBase message)
    {
        if (message is Command command)
        {
            if (command.CommandName == "notes_add")
            {
                using var scope = new TransactionScope();
                
                // var cacheLink = Guid.NewGuid();
                // _distributedCache.Set(cacheLink, );

                scope.Complete();
                
                return  Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }
}
