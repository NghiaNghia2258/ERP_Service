using ERP_Service.Application.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ERP_Service.Application.Services;

public class RedisCacheService : ICacheService
{
	private readonly IDatabase _database;

	public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
	{
		_database = connectionMultiplexer.GetDatabase();
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan expirationTime)
	{
		var serializedValue = JsonSerializer.Serialize(value);
		await _database.StringSetAsync(key, serializedValue, expirationTime);
	}

	public async Task<T> GetAsync<T>(string key)
	{
		var value = await _database.StringGetAsync(key);

		if (value.IsNullOrEmpty)
		{
			return default;
		}

		return JsonSerializer.Deserialize<T>(value);
	}

	public async Task<bool> RemoveAsync(string key)
	{
		return await _database.KeyDeleteAsync(key);
	}
}
