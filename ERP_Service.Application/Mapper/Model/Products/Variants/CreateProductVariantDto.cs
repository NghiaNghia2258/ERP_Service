namespace ERP_Service.Application.Mapper.Model.Products.Variants;

public class CreateProductVariantDto
{
	public double Price { get; set; }
	public string? ImageUrl { get; set; }

	public int Inventory { get; set; }
}
