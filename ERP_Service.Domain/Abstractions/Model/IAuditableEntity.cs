namespace ERP_Service.Domain.Abstractions.Model
{
	public interface IAuditableEntity : ICreateTracking, IUpdateTracking, ISoftDelete
	{
	}
}
