namespace ERP_Service.Application.Mapper.Model.Orders.Bundles;

public class CreateBundleDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double DiscountValue { get; set; }
    public bool IsPercentage { get; set; }
    public int? MaxUsageCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IEnumerable<int> ProductVariantIds { get; set; }
}
