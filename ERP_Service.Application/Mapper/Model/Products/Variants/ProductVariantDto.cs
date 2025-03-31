namespace ERP_Service.Application.Mapper.Model.Products.Variants;

public class ProductVariantDto
{
	public int? Id { get; set; }
	public double Price { get; set; }
	public string? ImageUrl { get; set; }

	public int Inventory { get; set; }
	public bool? IsDeleted { get; set; }
	public bool? IsEdited { get; set; }
	public int Version { get; set; }

}
