using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class OrderItem: EntityBase<int>
{
	public Guid OrderId { get; set; }

	public int ProductVariantId { get; set; }
	public string ImageUrl { get; set; } = null!;
	public int Quantity { get; set; }
	public double UnitPrice { get; set; }
	public bool? HasSelected { get; set; } = true;
	public virtual Order Order { get; set; } = null!;

	public virtual ProductVariant? ProductVariant { get; set; }
	
}
