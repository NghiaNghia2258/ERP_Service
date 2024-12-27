namespace ERP_Service.Domain.Abstractions.Model
{
	public interface IUpdateTracking
	{
		DateTime? UpdatedAt { get; set; }
		string? UpdatedBy { get; set; }
		string? UpdatedName { get; set; }
	}
}
