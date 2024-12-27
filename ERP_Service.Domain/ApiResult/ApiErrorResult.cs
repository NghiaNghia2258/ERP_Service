namespace ERP_Service.Domain.ApiResult;

public class ApiErrorResult : ApiResult
{
	public List<string> Errors { set; get; }
	public ApiErrorResult(int statuscode = 500, string message = "Something wrong happened. Please try again later") : base(false, message, statuscode)
	{
		Errors = new List<string>();
	}
	public ApiErrorResult(List<string> errs, int statuscode = 500, string message = "Something wrong happened. Please try again later") : base(false, message, statuscode)
	{
		Errors = errs;
	}
}
