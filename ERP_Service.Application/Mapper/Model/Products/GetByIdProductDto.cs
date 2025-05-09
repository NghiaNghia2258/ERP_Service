using ERP_Service.Application.Mapper.Model.Products.Variants;

namespace ERP_Service.Application.Mapper.Model.Products;

public class GetByIdProductDto
{
    public int Version { get; set; }
	public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CategoryId { get; set; }
    public string? BrandId { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsPhysicalProduct { get; set; }
    public decimal Weight { get; set; }
    public string UnitWeight { get; set; } = string.Empty;
    public string PropertyName1 { get; set; } = string.Empty;
    public string PropertyName2 { get; set; } = string.Empty;
    public List<string> PropertyValue1 { get; set; } = new();
    public List<string> PropertyValue2 { get; set; } = new();
    public List<VariantCreate> ProductVariants { get; set; } = new();
    public List<string> ExistingUrls { get; set; } = new();
    public List<ProductSpecificationAttribute> Specifications { get; set; } = new();
}
