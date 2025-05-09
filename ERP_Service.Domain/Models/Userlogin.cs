using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.Models.Stores;

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

	public virtual RoleGroup RoleGroup { get; set; } = null!;
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

}
