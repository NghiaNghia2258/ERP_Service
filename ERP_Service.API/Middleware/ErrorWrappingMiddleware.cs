using ERP_Service.Domain.ApiResult;
using System.Text.Json;

namespace ERP_Service.API.Middleware
{
	public class ErrorWrappingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorWrappingMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			var errorMsg = string.Empty;
			int statuscode = 0;
			try
			{
				// Random random = new Random();
				// int milliseconds = random.Next(100,300);
				// Thread.Sleep(milliseconds);
				await _next.Invoke(context);
			}
			catch (ArgumentException ex)
			{
				errorMsg = ex.Message;
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				statuscode = 4002;
			}
			catch (Exception ex)
			{
				errorMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				statuscode = 500;
			}
			if ((!context.Response.HasStarted && statuscode == StatusCodes.Status401Unauthorized) ||
				statuscode == StatusCodes.Status403Forbidden)
			{
				context.Response.ContentType = "application/json";

				var response = new ApiErrorResult(statuscode, "Unauthorized");

				var json = JsonSerializer.Serialize(response);

				await context.Response.WriteAsync(json);
			}

			else if (!context.Response.HasStarted && statuscode != StatusCodes.Status204NoContent &&
					 statuscode != StatusCodes.Status202Accepted &&
					 statuscode != StatusCodes.Status200OK &&
					 context.Response.ContentType != "text/html; charset=utf-8")
			{
				context.Response.ContentType = "application/json";

				var response = new ApiErrorResult(statuscode, errorMsg);

				var json = JsonSerializer.Serialize(response);

				await context.Response.WriteAsync(json);
			}
		}
	}
}