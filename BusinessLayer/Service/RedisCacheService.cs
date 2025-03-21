using System;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using BusinessLayer.Interface;

namespace BusinessLayer.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _cache;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _cache.StringGetAsync(key);
            return data.HasValue ? JsonSerializer.Deserialize<T>(data) : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _cache.StringSetAsync(key, jsonData, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
