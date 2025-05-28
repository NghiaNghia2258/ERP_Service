using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_Service.Domain.Models.Customers;

[Table("ShippingAddress")]
public class ShippingAddress: EntityBase<string>
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Order> Orders { get; set; }
}
