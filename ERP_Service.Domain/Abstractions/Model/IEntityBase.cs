namespace ERP_Service.Domain.Abstractions.Model
{
	public interface IEntityBase<TKey>
	{
		TKey Id { get; set; }
		int Version { get; set; }
	}
}
