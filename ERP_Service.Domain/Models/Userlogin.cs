using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;

public class UserLogin : EntityBase<int>, ISoftDelete
{
	public string Username { get; set; }
	public string Password { get; set; }
	public int RoleGroupId { get; set; }

	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }

	public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
	public virtual RoleGroup RoleGroup { get; set; } = null!;

}
