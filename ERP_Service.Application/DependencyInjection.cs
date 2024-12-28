using ERP_Service.Application.Services;
using ERP_Service.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
namespace ERP_Service.Application;

public static class DependencyInjection
{
	public static void AddApplicationServices(this IHostApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
		builder.Services.AddScoped<IAuthService, IdentityServices>();
		builder.Services.AddScoped<IAuthoziService, IdentityServices>();
		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
	}
}
