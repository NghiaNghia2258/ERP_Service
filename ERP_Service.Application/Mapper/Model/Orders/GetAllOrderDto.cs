namespace ERP_Service.Application.Mapper.Model.Orders;

public class GetAllOrderDto
{
	public Guid Id { get; set; }
	public string Code { get; set; } = null!;
	public string? Name { get; set; }
	public int Status { get; set; }
}
