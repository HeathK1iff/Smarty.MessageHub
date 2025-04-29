using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Smarty.MessageHub.Domain.Entities;

namespace Smarty.MessageHub.Infrastructure.Repository;

public abstract class EntityRepository<T> where T : EntityBase
{
    static object s_lockR = new();
    static object s_lockW = new();
    static Guid s_cachedKey = Guid.NewGuid();
    readonly IMemoryCache _memoryCache;
    readonly string _fileName;
    CancellationTokenSource? _tokenSource;
    

    public EntityRepository(IMemoryCache memoryCache, string fileName)
    {
        _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    protected void InsertOrUpdate(T entity)
    {
        var entities = new List<T>(GetAllOrEmpty());

        var foundUser = entities.FirstOrDefault(a => a.Id == entity.Id);

        if (foundUser is not null)
        {
            entities.Remove(foundUser);
        }

        if (entity.Clone() is T newUser)
        {
            entities.Add(newUser);
        }

        StoreToCache(entities.ToArray(), true);

    }

    protected T[] GetAllOrEmpty()
    {
        if (_memoryCache.TryGetValue<T[]>(s_cachedKey, out var entities))
        {
            return entities ?? Array.Empty<T>();
        }

        lock (s_lockR)
        {
            if (_memoryCache.TryGetValue<T[]>(s_cachedKey, out var entities2))
            {
                return entities2 ?? Array.Empty<T>();
            }

            if (!Path.Exists(_fileName))
            {
                return Array.Empty<T>();
            }

            using (var fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                var entities3 = JsonSerializer.Deserialize<T[]>(fs) ?? Array.Empty<T>();

                StoreToCache(entities3, false);

                return entities3;
            }
        }
    }

    protected void StoreToCache(T[] users, bool withExpiryToken)
    {
        lock (s_lockW)
        {
            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                .RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    if (reason != EvictionReason.TokenExpired)
                        return;

                    WriteAll(value as T[]);
                });

            if (withExpiryToken)
            {
                _tokenSource = new CancellationTokenSource();
                _tokenSource.CancelAfter(TimeSpan.FromSeconds(3));
                options.AddExpirationToken(new CancellationChangeToken(_tokenSource.Token));
            }

            _memoryCache.Set(s_cachedKey, users, options);
        }
    }

    private void WriteAll(T[]? entities)
    {
        lock (s_lockW)
        {
            if (entities is null)
            {
                return;
            }
            
            if (!string.IsNullOrWhiteSpace(_fileName))
            {
                using (var fs = new FileStream(_fileName, FileMode.CreateNew))
                {
                    var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entities));
                    fs.Write(content);
                    fs.Flush();
                }
            }
        }
    }
}
