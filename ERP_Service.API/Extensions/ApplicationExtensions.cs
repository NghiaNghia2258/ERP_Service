using ERP_Service.API.Middleware;

namespace ERP_Service.API.Extensions;
public static class ApplicationExtensions
{
    public static void UseInfrastructure(this IApplicationBuilder app)
    {

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.OAuthClientId("microservices_swagger");
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fashion API");
            c.DisplayRequestDuration();
        });
       
        app.UseMiddleware<ErrorWrappingMiddleware>();

        app.UseAuthentication();
        app.UseRouting();
        //app.UseHttpsRedirection(); //for production only
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });

    }
}
