namespace ERP_Service.Domain.ApiResult;

public class ApiSuccessResult : ApiResult
{
	public ApiSuccessResult(int statuscode = 200, string message = "Success") : base(true, message, statuscode)
	{
	}
}
public class ApiSuccessResult<T> : ApiSuccessResult
{
	private T _data;
	private long _fetchedRecordsCount;
	public T Data { get => _data; }
	public long FetchedRecordsCount
	{
		get
		{
			if (_fetchedRecordsCount <= 0) { return 1; }
			else { return _fetchedRecordsCount; }
		}
		set => _fetchedRecordsCount = value;
	}
	public int TotalRecordsCount { get; set; }
	public ApiSuccessResult(T data, int statuscode = 200, string message = "Success") : base(statuscode, message)
	{
		_data = data;
	}
}
