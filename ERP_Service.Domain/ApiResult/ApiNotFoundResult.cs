using ERP_Service.Domain.Const;

namespace ERP_Service.Domain.ApiResult;

public class ApiNotFoundResult: ApiErrorResult
{
	public ApiNotFoundResult(string message = "Not found") : base(StatusCodeCustom.NOTFOUND,message)
	{
		Errors = new List<string>();
	}
	public ApiNotFoundResult(List<string> errs, string message = "Not found") : base(errs,StatusCodeCustom.NOTFOUND, message)
	{
	}
}
