namespace ERP_Service.Application.Mapper.Model.Products;

public class GetByIdProductDto
{
    public int Version { get; set; }
	public int Id { get; set; }
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
    public double? OriginalPrice { get; set; }
    public double? Price { get; set; }
    public List<string> PropertyValue1 { get; set; } = new();
    public List<string> PropertyValue2 { get; set; } = new();
    public List<VariantCreate> ProductVariants { get; set; } = new();
    public List<string> ExistingUrls { get; set; } = new();
    public List<ProductSpecificationAttribute> Specifications { get; set; } = new();
}
