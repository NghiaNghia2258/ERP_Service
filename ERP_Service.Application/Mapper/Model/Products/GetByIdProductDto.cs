using ERP_Service.Application.Mapper.Model.Products.Images;
using ERP_Service.Application.Mapper.Model.Products.Variants;

namespace ERP_Service.Application.Mapper.Model.Products;

public class GetByIdProductDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	public string? NameEn { get; set; }

	public string? Description { get; set; }

	public string? MainImageUrl { get; set; }
	public int CategoryId { get; set; }
	public DateTime? CreatedAt { get; set; }
	public string? CreatedBy { get; set; }
	public string? CreatedName { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public string? UpdatedBy { get; set; }
	public string? UpdatedName { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
	public string? DeletedName { get; set; }

	public IEnumerable<ProductVariantDto> ProductVariants { get; set; }
	public IEnumerable<ProductImageDto> productImages { get; set; }
}
