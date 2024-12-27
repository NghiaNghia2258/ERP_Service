using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Services.Interfaces
{
	public interface IAuthoziService
	{
		Task IsAuthozi(HttpContext httpContext, string role = "");
	}
}
