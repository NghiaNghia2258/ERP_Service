using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Orders;

public partial class Order : EntityBase<Guid>, IAuditableEntity
{
	public string Code { get; set; } = null!;
	public string? Note { get; set; }
	public string? Name { get; set; }
	public Guid? CustomerId { get; set; }
	public Guid StoreId { get; set; }
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

	public Guid? RecipientsInformationId { get; set; }
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

	public virtual Customer? Customer { get; set; }

    public Store Store { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	public virtual Voucher? Voucher { get; set; }
	
}
