using MediatR;

namespace ERP_Service.Application.Mapper.Model.Customers;

public class CreateCustomerDto
{
	public string Name { get; set; } = null!;

	public string? Phone { get; set; }

	public string Gender { get; set; } = null!;
}
