using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Products;

public class ProductBrand : EntityBase<int>
{
    public Guid StoreId { get; set; } = new Guid();
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
