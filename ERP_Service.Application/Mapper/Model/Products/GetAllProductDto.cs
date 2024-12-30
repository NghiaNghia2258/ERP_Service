namespace ERP_Service.Application.Mapper.Model.Products;

public class GetAllProductDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string? MainImageUrl { get; set; }
	public double? TotalInventory { get; set; } = 0;
	public int CategoryId { get; set; }
	public string? CategoryName { get; set; }

}
