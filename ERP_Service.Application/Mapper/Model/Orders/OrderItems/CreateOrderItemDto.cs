namespace ERP_Service.Application.Mapper.Model.Orders.OrderItems;

public class CreateOrderItemDto
{
	public int ProductVariantId { get; set; }
	public Guid OrderId { get; set; }
	public int Quantity { get; set; }

	public double UnitPrice { get; set; }

	public double? DiscountPercent { get; set; }

	public double? DiscountValue { get; set; }
}
