using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Products;

public class ProductWish: EntityBase<int>
{
    public Guid CustomerId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public Customer Customers { get; set; }
}
