namespace ERP_Service.Domain.Abstractions.Model
{
	public interface ISoftDelete
	{
		bool IsDeleted { get; set; }
		DateTime? DeletedAt { get; set; }
		string? DeletedBy { get; set; }
		string? DeletedName { get; set; }
	}
}
