namespace ERP_Service.Domain.Abstractions.RepositoryBase
{
	public interface IReadBase<T, TKey>
	{
		IQueryable<T> FindAll(bool trackChanges = false);
		Task<T?> GetByIdAsync(TKey primaryKey);
		Task<T?> GetByIdAsync(TKey primaryKey, params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties);
	}
}
