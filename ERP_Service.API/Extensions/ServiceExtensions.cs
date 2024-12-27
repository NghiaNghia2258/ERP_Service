using ERP_Service.API.Configuration;
using ERP_Service.Application;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ERP_Service.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
  IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
            .Get<DatabaseSettings>();
        services.AddSingleton(databaseSettings);

        var apiConfiguration = configuration.GetSection(nameof(ApiConfiguration))
            .Get<ApiConfiguration>();
        services.AddSingleton(apiConfiguration);
        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();

		services.AddJwtAuthentication(configuration);
        services.AddCorsServices();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddAuthentication(ops =>
        {
            ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            ops.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(ops =>
        {
            ops.SaveToken = true;
            ops.RequireHttpsMetadata = false;
            ops.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = "User",
                ValidIssuer = "https://localhost:7112",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AVERYTOPSECRET123^%$"))
            };
        });
        return services;
    }
    public static IServiceCollection AddCorsServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        return services;
    }
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        var configuration = services.GetOptions<ApiConfiguration>("ApiConfiguration");
        if (configuration == null || string.IsNullOrEmpty(configuration.IssuerUri) ||
            string.IsNullOrEmpty(configuration.ApiName)) throw new Exception("ApiConfiguration is not configured!");

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Fashion API V1",
                    Version = configuration.ApiVersion
                });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter your token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Name = "Bearer"
                },
                new List<string>
                {
                    //"microservices_api.read",
                    //"microservices_api.write"
                }
            }
        });
        });
    }
}
