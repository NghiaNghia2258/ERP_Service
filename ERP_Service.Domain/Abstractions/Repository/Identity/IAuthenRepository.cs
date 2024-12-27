using ERP_Service.Domain.Models;
using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.Repository.Identity
{
	public interface IAuthenRepository
	{
		Task<UserLogin> SignIn(ParamasSignInRequest model);
		Task<bool> SignUp(ParamasSignUpRequest model);
	}
}
