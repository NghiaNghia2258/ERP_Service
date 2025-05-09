using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP_Service.Shared.Utilities;

public static class JwtTokenHelper
{
	public static string GenerateJwtToken(PayloadToken payloadToken, IConfiguration configuration)
	{
		if (payloadToken.Roles.Count() == 0)
		{
			throw new Exception("Unauthorized (Not login)");
		}
		var secretKey = configuration["JwtSettings:Key"];
		if (string.IsNullOrEmpty(secretKey))
		{
			throw new Exception("Secret key is not configured.");
		}
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new List<Claim>();
		claims.Add(new Claim("UserLoginId", payloadToken.UserLoginId.ToString()));
		claims.Add(new Claim("FullName", payloadToken.FullName ?? "No name"));
		claims.Add(new Claim("CustomerId", payloadToken.CustomerId.ToString()));
		claims.Add(new Claim("StoreId", payloadToken.StoreId.ToString()));
		claims.Add(new Claim("EmployeeId", payloadToken.EmployeeId.ToString()));
        foreach (var item in payloadToken.Roles)
		{
			if (!string.IsNullOrEmpty(item.Name))
			{
				claims.Add(new Claim(ClaimTypes.Role, item.Name));
			}
		}

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddYears(1),
		signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
	public static PayloadToken GetPayloadToken(HttpContext httpContext, IConfiguration configuration)
	{
		string bearerToken = httpContext.Request.Headers["Authorization"].FirstOrDefault() ?? string.Empty;
		if (string.IsNullOrEmpty(bearerToken))
		{
			return new();
		}
		else
		{
			return VerifyJwtToken(bearerToken.Split(" ")[1], configuration);
		}
	}
	public static PayloadToken VerifyJwtToken(string token, IConfiguration configuration)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var secretKey = configuration["JwtSettings:Key"];
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Secret key is not configured.")));
		var validationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = securityKey,
			ValidateLifetime = false
		};

		var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
		JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
        var customerIdStr = jwtToken.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;
        var storeIdStr = jwtToken.Claims.FirstOrDefault(c => c.Type == "StoreId")?.Value;
        var employeeIdStr = jwtToken.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;

        Guid.TryParse(customerIdStr, out Guid customerId);
        Guid.TryParse(storeIdStr, out Guid storeId);
        Guid.TryParse(employeeIdStr, out Guid employeeId);
        PayloadToken payloadToken = new PayloadToken()
		{
			UserLoginId = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == "UserLoginId")?.Value ?? throw new Exception("UserLoginId claim not found")),
			FullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value ?? "No name",
            CustomerId = customerId,
            StoreId = storeId,
            EmployeeId = employeeId,
        };

		return payloadToken;
	}
}
