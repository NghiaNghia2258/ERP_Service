using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Products;


public partial class ProductRate : EntityBase<int>
{

    public int ProductId { get; set; }

    public Guid CustomerId { get; set; }

    public int Rating { get; set; } = 0;

    public string? Review { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
