using Microsoft.Extensions.Caching.Distributed;
using TechOnIt.Infrastructure.Caching.Contracts;

namespace TechOnIt.Infrastructure.Caching;

public class RedisService: IRedisService
{
    private readonly IFallbackCache _fallbackCache;
    private readonly IDistributedCache _distributedCache;
    public RedisService(IDistributedCache distributedCache, IFallbackCache fallbackCache)
    {
        _distributedCache = distributedCache;
        _fallbackCache = fallbackCache;
    }

    public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions();
        options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
        options.SlidingExpiration = unusedExpireTime;

        var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
        await _distributedCache.SetStringAsync(recordId, jsonData, options);
        await _fallbackCache.SetRecordAsync(recordId, jsonData);
    }

    public async Task<T> GetRecordAsync<T>(string recordId)
    {
        string jsonData = string.Empty;

        try
        {
            jsonData = await _distributedCache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                return default(T);
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);
        }
        catch (Exception exp)
        {
            return await _fallbackCache.GetRecordAsync<T>(recordId);
        }
    }
}