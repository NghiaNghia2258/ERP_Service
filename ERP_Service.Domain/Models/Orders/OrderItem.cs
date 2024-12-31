using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class OrderItem: EntityBase<int>, IAuditableEntity
{
	public Guid OrderId { get; set; }

	public int ProductVariantId { get; set; }
	public string ImageUrl { get; set; } = null!;

	public int Quantity { get; set; }

	public double UnitPrice { get; set; }

	public double? DiscountPercent { get; set; }

	public double? DiscountValue { get; set; }
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

	public virtual Order Order { get; set; } = null!;

	public virtual ProductVariant? ProductVariant { get; set; }
	
}
