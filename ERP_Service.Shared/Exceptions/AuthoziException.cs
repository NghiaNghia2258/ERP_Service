namespace ERP_Service.Shared.Exceptions;

public class AuthoziException: Exception
{
	public AuthoziException() : base("403 Not authentication") { }
	public AuthoziException(string message) : base(message) { }
	public AuthoziException(string message, Exception inner) : base(message, inner) { }
}
