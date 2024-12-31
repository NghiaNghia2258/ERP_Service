using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Orders;

public class Voucher: EntityBase<Guid>, IAuditableEntity
{
	public string VoucherCode { get; set; } = null!;
	public string? Title { get; set; }
	public string? Description { get; set; }

	public double? DiscountPercent { get; set; }

	public double? DiscountValue { get; set; }
	public double? MaxDiscountValue { get; set; }
	public double? MinOrderValue { get; set; }

	public int? UsedCount { get; set; }
	public int? UsageLimit { get; set; }

	public DateTime StartDate { get; set; }
	public DateTime ExpirationDate { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public string CreatedBy { get; set; } = null!;
	public string CreatedName { get; set; } = null!;
	public DateTime? UpdatedAt { get; set; }
	public string? UpdatedBy { get; set; }
	public string? UpdatedName { get; set; }
	public bool IsDeleted { get; set; } = false;
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }

	public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
