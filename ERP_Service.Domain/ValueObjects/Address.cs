namespace ERP_Service.Domain.ValueObjects;

public record Address
{
	public string Street { get; init; } = null!;
	public string City { get; init; } = null!;
	public string State { get; init; } = null!;
	public string ZipCode { get; init; } = null!;
	public string Country { get; init; } = null!;
}
