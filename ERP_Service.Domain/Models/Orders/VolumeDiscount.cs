using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class VolumeDiscount : EntityBase<int>
{
    public int MinQuantity { get; set; }
    public double DiscountValue { get; set; }
    public bool IsPercentage { get; set; }
    public virtual ICollection<ProductVariant> ProductVariants { get; set; }
}
