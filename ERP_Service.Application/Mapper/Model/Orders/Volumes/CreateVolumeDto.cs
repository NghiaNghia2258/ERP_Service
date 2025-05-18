namespace ERP_Service.Application.Mapper.Model.Orders.Volumes;

public class CreateVolumeDto
{
    public bool? IsApplyForAll { get; set; }
    public int MinQuantity { get; set; }
    public double DiscountValue { get; set; }
    public bool IsPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IEnumerable<int>? ProductVariantIds { get; set; }
    public IEnumerable<int>? ExcludeProductVariantIds { get; set; }

}
