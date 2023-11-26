namespace TechOnIt.Infrastructure.Caching.Contracts;

public interface IRedisService
{
    Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
    Task<T> GetRecordAsync<T>(string recordId);
}
