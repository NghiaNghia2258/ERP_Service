using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Abstractions.Repository.Identity;
using ERP_Service.Domain.Models;
using ERP_Service.Shared.Exceptions;
using ERP_Service.Shared.Models;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Application.Services;

public class IdentityServices : IAuthService, IAuthoziService
{
	private readonly IAuthenRepository _authenRepository;
	private readonly IAuthoziRepository _authoziRepository ;
	protected readonly IConfiguration _config;
	private readonly IHttpContextAccessor _httpContextAccessor;


	public PayloadToken PayloadToken => JwtTokenHelper.GetPayloadToken(_httpContextAccessor.HttpContext, _config);

	public IdentityServices(IAuthenRepository authenRepository, IAuthoziRepository authoziRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
	{
		_authenRepository = authenRepository;
		_authoziRepository = authoziRepository;
		_config = config;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<PayloadToken> SignIn(ParamasSignInRequest paramas)
	{
		UserLogin userlogin = await _authenRepository.SignIn(paramas);
		if (userlogin.Username == null) {
			return new PayloadToken();
		}
		PayloadToken payloadToken = new PayloadToken();
		payloadToken.Username = userlogin.Username;
		payloadToken.UserLoginId = userlogin.Id;
        payloadToken.CustomerId = userlogin.Customers.Any() ? userlogin.Customers.First().Id : new();
        payloadToken.StoreId = userlogin.Stores.Any() ? userlogin.Stores.First().Id : new();
        payloadToken.EmployeeId = userlogin.Employees.Any() ? userlogin.Employees.First().Id : new();

        List<RoleDto> roles = new List<RoleDto>();
		foreach(var item in userlogin.RoleGroup.Roles)
		{
			roles.Add(new RoleDto
			{
				Id = item.Id,
				Name = item.Name,
			});
		}
		payloadToken.Roles = roles;
		return payloadToken;
	}

	public async Task<bool> SignUp(ParamasSignUpRequest paramas)
	{
		bool isSignUpSuccess = await _authenRepository.SignUp(paramas);
		return isSignUpSuccess;
	}

	public async Task IsAuthozi(string role = "")
	{
		PayloadToken payload = JwtTokenHelper.GetPayloadToken(_httpContextAccessor.HttpContext, _config);
		bool isAuthozi = await _authoziRepository.IsAuthozi(payload.UserLoginId,role);
		if(!isAuthozi)
		{
			throw new AuthoziException("Xác thực token thất bại vui lòng đọc file readme.md để biết thêm chi tiết về model phân quyền");
		}
	}
	
}
