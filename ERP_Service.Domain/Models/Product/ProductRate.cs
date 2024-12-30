using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;


public partial class ProductRate : EntityBase<int>
{

	public int ProductId { get; set; }

	public Guid CustomerId { get; set; }

	public int? Rating { get; set; }

	public string? Review { get; set; }

	public virtual Customer Customer { get; set; } = null!;

	public virtual Product Product { get; set; } = null!;
}
