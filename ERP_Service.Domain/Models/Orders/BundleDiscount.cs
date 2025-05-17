using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.Orders;

public class BundleDiscount
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double DiscountValue { get; set; }
    public bool IsPercentage { get; set; }
    public int? MaxUsageCount { get; set; }
    public int UsageCount { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<ProductVariant> ProductVariants { get; set; }
}
