using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Exceptions.Products;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Products;

public partial class Product : EntityBase<int>, IAuditableEntity
{
    public Guid StoreId { get; set; } = new Guid();
    public string Name { get; set; } = null!;
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? MainImageUrl { get; set; }
    public string? ImageUrls { get; set; }
    public double? TotalInventory { get; set; } = 0;
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? BrandId { get; set; }
    public int SellCount { get; set; } = 0;
    public string? BrandName { get; set; }
    public bool? IsPhysicalProduct { get; set; }
    public double? Weight { get; set; }
    public string? UnitWeight { get; set; }
    public string? PropertyName1 { get; set; }
    public string? PropertyName2 { get; set; }
    public string? PropertyValue1 { get; set; }
    public string? PropertyValue2 { get; set; }
    public string? Specifications { get; set; }
    public double? OriginalPrice { get; set; }
    public double? Price { get; set; }
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

    public double Rate => ProductRates.Count > 0 ? ProductRates.Average(x => x.Rating) : 0;
    public double RateCount => ProductRates.Count;

    public void DeductInventory(int quantity)
    {
        if (quantity > TotalInventory) {
            throw new OutOfStockException();
		}
		TotalInventory -= quantity;
	}

	public virtual ProductCategory Category { get; set; } = null!;
	public virtual ProductBrand Brand { get; set; } = null!;
    public virtual Store Store { get; set; } = null!;
    public virtual ICollection<ProductRate> ProductRates { get; set; } = new List<ProductRate>();
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    public virtual ICollection<ProductWish> ProductWishs { get; set; } = new List<ProductWish>();

}
