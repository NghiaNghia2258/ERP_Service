namespace ERP_Service.Domain.ApiResult;

public class ApiResult
{
	protected readonly string MESSAGE_SUCCESS = "Success";
	protected readonly string MESSAGE_ERROR = "Error";
	protected readonly string MESSAGE_NOTFOUND = "Not found";

	public int StatusCode { get; set; }
	public bool IsSucceeded { get; set; }
	public string Message { get; set; }
	public ApiResult(bool isSucceeded, string message, int statusCode)
	{
		Message = message;
		IsSucceeded = isSucceeded;
		StatusCode = statusCode;
	}
	public ApiResult(bool isSucceeded, int statusCode)
	{
		IsSucceeded = isSucceeded;
		StatusCode = statusCode;
	}
	public ApiResult(bool isSucceeded)
	{
		IsSucceeded = isSucceeded;
	}
}
