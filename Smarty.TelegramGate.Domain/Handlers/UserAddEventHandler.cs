using MessagePack;
using Microsoft.Extensions.Caching.Distributed;
using Smarty.Shared.EventBus.Abstractions.Events;
using Smarty.Shared.EventBus.Abstractions.Interfaces;
using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Events;
using Smarty.TelegramGate.Domain.Interfaces;

namespace Smarty.TelegramGate.Infrastructure.Handlers;

public sealed class UserAddEventHandler : IEventHandler
{
    readonly IUserRepository _usersRepository;
    readonly IDistributedCache _destributedCache;

    public UserAddEventHandler(IUserRepository usersRepository, IDistributedCache destributedCache)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _destributedCache = destributedCache ?? throw new ArgumentNullException(nameof(destributedCache));
    }

    public async Task ReceivedAsync(EventBase @event, CancellationToken cancellationToken)
    {
        if (@event is not UserAddEvent userAddEvent)
        {
            return;
        }

        string key = userAddEvent.CacheObjectId.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        var cachedObj = await _destributedCache.GetAsync(key, cancellationToken);

        UserDto userDto = MessagePackSerializer.Deserialize<UserDto>(cachedObj);

        var user = new User()
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            UserLogin = userDto.UserLogin,
            Contacts = userDto.Contacts?
                .Where(a => a.UseForNotification == true)
                .Select(a => new UserContact()
                {
                    ContactInformation = a.ContactInformation,
                    Type = string.Equals(a.Type, "telegram", StringComparison.OrdinalIgnoreCase) ? ContactType.Telegram : ContactType.Undefined
                }).ToArray() ?? Array.Empty<UserContact>()
        };

        _usersRepository.InsertOrUpdate(user);
    }

    [MessagePackObject]
    public sealed class UserDto
    {
        [Key(0)]
        public Guid Id { get; init; }
        [Key(1)]
        public string? FirstName { get; init; }
        [Key(2)]
        public string? LastName { get; init; }
        [Key(3)]
        public string? UserLogin { get; init; }
        [Key(4)]
        public UserContactDto[]? Contacts { get; init; }
        [Key(5)]
        public string? Status { get; init; }
        [Key(6)]
        public DateTime Expired { get; init; }
    }

    [MessagePackObject]
    public sealed class UserContactDto
    {
        [Key(0)]
        public string? Type { get; init; }
        [Key(1)]
        public string? ContactInformation { get; init; }
        [Key(2)]
        public bool UseForNotification { get; init; }
    }
}
