namespace ERP_Service.Application.Mapper.Model.Products;

public class ProductSpecificationAttribute
{
    public string AttributeName { get; set; } = string.Empty; 
    public string AttributeValue { get; set; } = string.Empty;
}
public class VariantCreate
{
    public string PropertyValue1 { get; set; } = string.Empty;
    public string PropertyValue2 { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActivate { get; set; }
    public string? Image { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; } = null!;
    public string? CreatedName { get; set; } = null!;
}
public class CreateProductDto
{
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
    public List<string> RemovedUrls { get; set; } = new();
    public List<string> ExistingUrls { get; set; } = new();
    public List<ProductSpecificationAttribute> Specifications { get; set; } = new();
}
