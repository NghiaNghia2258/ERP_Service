namespace ERP_Service.Domain.Abstractions.RepositoryBase
{
	public interface IRepositoryBase<T, TKey> : IReadBase<T, TKey>, IWriteBase<T, TKey> where T : EntityBase<TKey>
	{
	}
}
