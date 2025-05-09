namespace ERP_Service.Application.Mapper.Model.Products;

public class GetAllProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string MainImageUrl { get; set; } = string.Empty;
    public double TotalInventory { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public double Rate { get; set; }
    public double RateCount { get; set; }
    public string PropertyName1 { get; set; } = string.Empty;
    public string PropertyName2 { get; set; } = string.Empty;

    public List<ResponseGetAllProductVariant> ProductVariants { get; set; } = new();

}
public class ResponseGetAllProductVariant
{
    public string PropertyValue1 { get; set; } = string.Empty;
    public string PropertyValue2 { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Inventory { get; set; }
}