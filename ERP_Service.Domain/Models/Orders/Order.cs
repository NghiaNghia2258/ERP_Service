using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Orders;

public partial class Order : EntityBase<Guid>, IAuditableEntity
{
	public string Code { get; set; }
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

	public Guid? RecipientsInformationId { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public string CreatedName { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public string? UpdatedBy { get; set; }
	public string? UpdatedName { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }

	public virtual Customer? Customer { get; set; }

	public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

	public virtual RecipientsInformation? RecipientsInformation { get; set; }

	public virtual Voucher? Voucher { get; set; }
	
}
