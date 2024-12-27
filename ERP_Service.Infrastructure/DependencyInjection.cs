using ERP_Service.DAL.Repostiroty;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Repository.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ERP_Service.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructureServices(this IHostApplicationBuilder builder, IConfiguration configuration)
	{
		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(configuration["DatabaseSettings:ConnectionString"],
				builder =>
					builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
					;
		});
		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
		builder.Services.AddScoped<IAuthenRepository, IdentityRepository>();
		builder.Services.AddScoped<IAuthoziRepository, IdentityRepository>();
	}

}
