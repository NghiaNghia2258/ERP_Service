using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;

public partial class ProductCategory : EntityBase<int>
{
	public string Name { get; set; } = null!;

	public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
