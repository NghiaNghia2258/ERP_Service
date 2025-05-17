using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class BundleDiscountItem : EntityBase<int>
{
    public int ProductVariantId { get; set; }
    public int BundleDiscountId { get; set; }
    public BundleDiscount BundleDiscount { get; set; }
    public ProductVariant ProductVariant { get; set; }
}
