using Microsoft.Extensions.Caching.Distributed;
using University.Interfaces;

namespace University.Implementations
{
    public class DistributedCacheWrapper : IDistributedCacheWrapper
    {
        private readonly IDistributedCache _distributedCache;
        public DistributedCacheWrapper(IDistributedCache distributedCache) 
        { 
            _distributedCache = distributedCache;
        }

        public async Task<string?> GetStringAsync(string cacheKey)
        {
            return await _distributedCache.GetStringAsync(cacheKey);
        }

        public async Task<string?> UpdateStringAsync(string cacheKey, string data)
        {
            await _distributedCache.SetStringAsync(cacheKey, data, 
                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = new TimeSpan(TimeSpan.TicksPerDay) });

            return await GetStringAsync(cacheKey);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _distributedCache.RemoveAsync(cacheKey);
        }
    }
}
