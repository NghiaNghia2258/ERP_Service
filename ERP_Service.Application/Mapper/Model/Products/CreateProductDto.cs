using ERP_Service.Application.Mapper.Model.Products.Images;
using ERP_Service.Application.Mapper.Model.Products.Variants;

namespace ERP_Service.Application.Mapper.Model.Products;

public class CreateProductDto
{
	public string Name { get; set; } = null!;

	public string? NameEn { get; set; }

	public string? Description { get; set; }
	public string? MainImageUrl { get; set; }

	public int CategoryId { get; set; }
	public string? CategoryName { get; set; }

	public int TotalInventory => CreateProductVariants.Sum(x => x.Inventory);

	public IEnumerable<CreateProductVariantDto> CreateProductVariants { get; set; } = new List<CreateProductVariantDto>();
	public IEnumerable<ProductImageDto> CreateProductImages { get; set; } = new List<ProductImageDto>();
}
