namespace Fashion.API.Extensions;

public static class HostExtensions
{
    public static void AddAppConfigurations(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        builder.Configuration.AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();
    }
}
