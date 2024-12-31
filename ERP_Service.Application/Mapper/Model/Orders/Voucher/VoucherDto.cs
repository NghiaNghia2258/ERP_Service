namespace ERP_Service.Application.Mapper.Model.Orders.Voucher;

public class VoucherDto
{
	public Guid? Id { get; set; }
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
}
