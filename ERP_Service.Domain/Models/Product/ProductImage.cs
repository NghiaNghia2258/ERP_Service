using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;

public partial class ProductImage : EntityBase<int>
{

	public string ImageUrl { get; set; } = null!;

	public int ProductId { get; set; }

	public virtual Product Product { get; set; } = null!;
}
