namespace ERP_Service.Application.Services.Interfaces;

public interface ICacheService
{
	Task SetAsync<T>(string key, T value, TimeSpan expirationTime);
	Task<T> GetAsync<T>(string key);
	Task<bool> RemoveAsync(string key);
}
