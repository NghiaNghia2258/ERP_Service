using ERP_Service.Domain.Abstractions.Repository.Identity;
using ERP_Service.Domain.Models;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.DAL.Repostiroty
{
	public class IdentityRepository :RepositoryBase<UserLogin,int>, IAuthoziRepository, IAuthenRepository
	{
		public IdentityRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<bool> IsAuthozi(int userLoginId, string? role = null)
		{
			UserLogin? UserLogin = await _dbContext.UserLogins
				.Include(x => x.RoleGroup)
				.ThenInclude(x => x.Roles.Where(y => y.Name == role))
				.Where(x => x.Id == userLoginId)
				.Select(x => new UserLogin()
				{
					RoleGroup = x.RoleGroup
				}).FirstOrDefaultAsync();
			return UserLogin.RoleGroup.Roles.Count > 0;
		}

		public async Task<UserLogin> SignIn(ParamasSignInRequest model)
		{
			UserLogin? UserLogin = await _dbContext.UserLogins
				.Include(x => x.RoleGroup)
				.ThenInclude(x => x.Roles)
				.Where(x => x.Username == model.Username && x.Password == model.Password)
				.Select(x => new UserLogin()
				{
					Id = x.Id,
					Username = x.Username,
					RoleGroupId = x.RoleGroupId,
					RoleGroup = x.RoleGroup
				}).FirstOrDefaultAsync();
				;
			return UserLogin ?? new UserLogin();
		}

		public async Task<bool> SignUp(ParamasSignUpRequest model)
		{
			UserLogin newUser = new UserLogin() { 
				Username= model.Username,
				Password = model.Password,
				RoleGroupId = model.RoleGroupId,
			};
			await CreateAsync(newUser);
			return true;
		}
	}
}
