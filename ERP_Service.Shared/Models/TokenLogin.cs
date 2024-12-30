namespace ERP_Service.Shared.Models;

public class TokenLogin
{
	public string AccessToken { get; set; } = null!;
	public string RefreshToken { get; set; }= null!;
}
