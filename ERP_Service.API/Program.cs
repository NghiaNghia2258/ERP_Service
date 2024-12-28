using ERP_Service.API.Extensions;
using ERP_Service.Application;
using ERP_Service.Infrastructure;
using Fashion.API.Extensions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
	builder.AddInfrastructureServices(builder.Configuration);
	builder.AddApplicationServices();
	builder.Host.AddAppConfigurations();
	builder.Services.AddConfigurationSettings(builder.Configuration);
	builder.Services.AddInfrastructure(builder.Configuration);

	var app = builder.Build();
	app.UseCors("CorsPolicy");
	app.UseInfrastructure();

	app.Run();
}

catch (Exception ex)
{
	var type = ex.GetType().Name;
	if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

	Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
	Log.Information($"Shutdown {builder.Environment.ApplicationName} complete");
	Log.CloseAndFlush();
}