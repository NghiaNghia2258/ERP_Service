using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Products;

public partial class ProductVariant : EntityBase<int>, IAuditableEntity
{
    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public double Price { get; set; }
    public string? ImageUrl { get; set; }

    public int Inventory { get; set; } = 0;
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string CreatedName { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? UpdatedName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeletedName { get; set; }

    public virtual Product? Product { get; set; }

}
