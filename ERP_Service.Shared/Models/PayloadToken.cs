namespace ERP_Service.Shared.Models;

public class PayloadToken
{
	public int UserLoginId { get; set; }
	public string Username { get; set; }
	public string FullName { get; set; }

	public IEnumerable<RoleDto> Roles { get; set; }
}
