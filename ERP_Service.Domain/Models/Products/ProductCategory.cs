using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Products;

public partial class ProductCategory : EntityBase<int>
{
    public Guid StoreId { get; set; } = new Guid();
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
