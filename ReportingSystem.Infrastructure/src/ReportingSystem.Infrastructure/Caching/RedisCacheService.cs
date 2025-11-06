using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ReportingSystem.Application.Common.Interfaces;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ReportingSystem.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<RedisCacheService> _logger;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public RedisCacheService(IDistributedCache distributedCache, ILogger<RedisCacheService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
                if (string.IsNullOrEmpty(cachedValue))
                {
                    return null;
                }

                return JsonSerializer.Deserialize<T>(cachedValue, _jsonSerializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize cached value for key {CacheKey}.", key);
                // Consider removing the invalid cache entry
                await RemoveAsync(key, cancellationToken);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting value from cache for key {CacheKey}.", key);
                // Don't re-throw; cache failures should not crash the application.
                return null;
            }
        }

        public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                var jsonValue = JsonSerializer.Serialize(value, _jsonSerializerOptions);
                await _distributedCache.SetStringAsync(key, jsonValue, options, cancellationToken);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to serialize value for caching with key {CacheKey}.", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while setting value in cache for key {CacheKey}.", key);
                // Don't re-throw; cache failures should not crash the application.
            }
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing value from cache for key {CacheKey}.", key);
                // Don't re-throw; cache failures should not crash the application.
            }
        }
    }
}