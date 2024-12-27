namespace ERP_Service.Domain.Abstractions.Repository.Identity;

public interface IAuthoziRepository
{
	Task<bool> IsAuthozi(int userLoginId, string? role = null);
}
