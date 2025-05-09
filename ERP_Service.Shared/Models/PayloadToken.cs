namespace ERP_Service.Shared.Models;

public class PayloadToken
{
	public int UserLoginId { get; set; }
	public string Username { get; set; }= null!;
	public string FullName { get; set; }= null!;
	public Guid CustomerId { get; set; }
	public Guid StoreId { get; set; }
	public Guid EmployeeId { get; set; }

	public IEnumerable<RoleDto> Roles { get; set; } = null!;
}
