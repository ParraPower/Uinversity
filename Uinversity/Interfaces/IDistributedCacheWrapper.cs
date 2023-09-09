namespace University.Interfaces
{
    public interface IDistributedCacheWrapper
    {
        public Task<string?> GetStringAsync(string cacheKey);
        public Task<string?> UpdateStringAsync(string cacheKey, string data);
        public Task RemoveAsync(string cacheKey);
    }
}
