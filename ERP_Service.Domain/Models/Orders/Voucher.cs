using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Exceptions.Orders;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Orders;

public class Voucher: EntityBase<Guid>
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

	public Guid? StoreId { get; set; }
	public Store? Store { get;set; }

	public void Use()
	{
		if (UsedCount > UsageLimit)
		{
			throw new UseVocherException("Hết lượt sử dụng");
		}
		else if (StartDate > DateTime.Now)
		{
			throw new UseVocherException("Chưa đến ngày sử dụng");
		}
		else if (ExpirationDate >= DateTime.Now)
		{
			throw new UseVocherException("Hết hạn sử dụng");
		}
		else
		{
			UsedCount++;
		}
	}

	public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
