namespace ERP_Service.Domain.Exceptions.Orders;

public class UseVocherException : Exception
{
	public UseVocherException() : base("Use voucher fail") { }
	public UseVocherException(string message) : base(message) { }
	public UseVocherException(string message, Exception inner) : base(message, inner) { }
}
