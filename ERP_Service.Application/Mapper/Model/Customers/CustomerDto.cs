namespace ERP_Service.Application.Mapper.Model.Customers;

public class CustomerDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Phone { get; set; }

	public string Gender { get; set; } = null!;
	public int Point { get; set; } = 0;
	public double Debt { get; set; } = 0;

	public Guid? UserLoginId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public string? CreatedBy { get; set; }
	public string? CreatedName { get; set; }
	public bool IsDeleted { get; set; } = false;
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }
	public int Versiton { get; set; }
}
