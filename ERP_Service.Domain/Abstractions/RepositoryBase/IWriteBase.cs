using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.RepositoryBase;

public interface IWriteBase<T, TKey>
{
	Task<TKey> CreateAsync(T entity, PayloadToken payloadToken);
	Task DeleteAsync(TKey entity, PayloadToken payloadToken);
	Task UpdateAsync(T update, PayloadToken payloadToken);
}
