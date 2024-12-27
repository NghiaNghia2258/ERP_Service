using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;

public class Role: EntityBase<int>
{
	public string Name { get; set; } = null!;

	public virtual ICollection<RoleGroup> RoleGroups { get; set; } = new List<RoleGroup>();
}
