 using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models;


public class Customer : EntityBase<Guid>, ICreateTracking, ISoftDelete
{
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public bool? IsActive { get; set; }

    public string Gender { get; set; } = null!;
	public int Point { get; set; } = 0;
	public double Debt { get; set; } = 0;

	public int? UserLoginId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public string? CreatedBy { get; set; }
	public string? CreatedName { get; set; }
	public bool IsDeleted { get; set; } = false;
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }
	public Guid StoreId { get; set; }
    public UserLogin UserLogin { get; set; }
    public virtual Store Store { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
	public virtual ICollection<ProductRate> ProductRates { get; set; } = new List<ProductRate>();
	public virtual ICollection<ProductWish> ProductWishs { get; set; } = new List<ProductWish>();
}
