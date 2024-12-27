namespace ERP_Service.Domain.Abstractions.Model
{
	public interface ICreateTracking
	{
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string CreatedName { get; set; }
	}
}
