using Microsoft.Extensions.Caching.Memory;
using TechOnIt.Infrastructure.Caching.Contracts;

namespace TechOnIt.Infrastructure.Caching;
public class FallbackCache : IFallbackCache
{
    private readonly IMemoryCache _memoryCache;
    public FallbackCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var options = new MemoryCacheEntryOptions();
        options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
        options.SlidingExpiration = unusedExpireTime;

        await Task.FromResult(_memoryCache.Set(recordId, System.Text.Json.JsonSerializer.Serialize(data), options));
    }

    public async Task<T> GetRecordAsync<T>(string recordId)
    {
        var jsonData = await Task.FromResult(_memoryCache.Get<string>(recordId));
        return jsonData is null ? default(T) : System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);
    }
}