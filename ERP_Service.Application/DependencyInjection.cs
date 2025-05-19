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
		//builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
		//{
		//	var configuration = ConfigurationOptions.Parse(builder.Configuration.GetSection("Redis:ConnectionString").Value, true);
		//	return ConnectionMultiplexer.Connect(configuration);
		//});
		builder.Services.AddMemoryCache();
		builder.Services.AddScoped<IAuthService, IdentityServices>();
		builder.Services.AddScoped<IAuthoziService, IdentityServices>();
		builder.Services.AddScoped<ICacheService, MemoryCacheService>();
		builder.Services.AddSingleton<IEventBufferService, FileEventBufferService>();
		builder.Services.AddHostedService<EventLogProcessorService>();
        //builder.Services.AddScoped<ICacheService, RedisCacheService>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
	}
}
