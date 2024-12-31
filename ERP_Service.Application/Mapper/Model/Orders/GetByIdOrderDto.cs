using ERP_Service.Application.Mapper.Model.Orders.OrderItems;

namespace ERP_Service.Application.Mapper.Model.Orders;

public class GetByIdOrderDto
{
	public string Code { get; set; } = null!;
	public string? Note { get; set; }
	public string? Name { get; set; }
	public Guid CustomerId { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerPhone { get; set; }
	public string? CustomerNote { get; set; }

	public int PaymentStatus { get; set; } = 1!;

	public double Tax { get; set; } = 0!;

	public double? DiscountPercent { get; set; } = 0!;

	public double? DiscountValue { get; set; } = 0!;
	public double TotalPrice { get; set; } = 0!;
	public Guid? VoucherId { get; set; }
	public string? VoucherCode { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; } = null!;
	public string CreatedName { get; set; } = null!;
	public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

}
