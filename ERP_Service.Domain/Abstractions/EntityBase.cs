using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Abstractions;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
	public TKey Id { get; set; }
	public int Version { get; set; } = 0;
}
