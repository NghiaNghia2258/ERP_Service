using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class VolumeDiscountItem: EntityBase<int>
{
    public int ProductVariantId { get; set; }
    public int VolumeDiscountId { get; set; }
    public VolumeDiscount VolumeDiscount { get; set; }
    public ProductVariant ProductVariant { get; set; }
}
