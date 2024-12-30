namespace ERP_Service.Application.Mapper.Model.Products.Images;

public class ProductImageDto
{
	public int? Id { get; set; }
	public string? ImageUrl { get; set; } = null!;
	public bool? IsDeleted { get; set; }
}
