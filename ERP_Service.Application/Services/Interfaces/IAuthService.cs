using ERP_Service.Shared.Models;

namespace ERP_Service.Application.Services.Interfaces;
public interface IAuthService
{
	Task<PayloadToken> SignIn(ParamasSignInRequest paramas);
	Task<bool> SignUp(ParamasSignUpRequest paramas);
}
