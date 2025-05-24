using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Orders;

public class Cart : EntityBase<int>
{
    public Guid CustomerId { get; set; }
    public bool HasOrder { get; set; } = false;
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual Customer Customer { get; set; }
}
