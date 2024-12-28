using ERP_Service.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ERP_Service.Application.Services;

public class MemoryCacheService : ICacheService
{
	private readonly IMemoryCache _memoryCache;

	public MemoryCacheService(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan expirationTime)
	{
		_memoryCache.Set(key, value, expirationTime);
		await Task.CompletedTask;
	}

	public async Task<T> GetAsync<T>(string key)
	{
		if (_memoryCache.TryGetValue(key, out T value))
		{
			return await Task.FromResult(value);
		}

		return await Task.FromResult(default(T));
	}

	public async Task<bool> RemoveAsync(string key)
	{
		_memoryCache.Remove(key);
		return await Task.FromResult(true);
	}
}
