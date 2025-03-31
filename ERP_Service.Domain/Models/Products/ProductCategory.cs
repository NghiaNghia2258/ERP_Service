using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Products;

public partial class ProductCategory : EntityBase<int>
{
    public string Name { get; set; } = null!;
    public Guid StoreId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual Store Store { get; set; } = null!;
}
