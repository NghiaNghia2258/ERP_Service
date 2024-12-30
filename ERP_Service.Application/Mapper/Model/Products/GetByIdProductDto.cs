using ERP_Service.Application.Mapper.Model.Products.Images;
using ERP_Service.Application.Mapper.Model.Products.Variants;

namespace ERP_Service.Application.Mapper.Model.Products;

public class GetByIdProductDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	public string? NameEn { get; set; }

	public string? Description { get; set; }
	public double? TotalInventory => ProductVariants.Sum(x => x.Inventory);
	public string? MainImageUrl { get; set; }
	public int CategoryId { get; set; }
	public int Version { get; set; }

	public IEnumerable<ProductVariantDto> ProductVariants { get; set; } = null!;
	public IEnumerable<ProductImageDto> productImages { get; set; } = null!;
}
