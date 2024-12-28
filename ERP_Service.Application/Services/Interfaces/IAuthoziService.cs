using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Services.Interfaces
{
	public interface IAuthoziService
	{
		PayloadToken PayloadToken { get; }
		Task IsAuthozi(string role = "");
	}
}
