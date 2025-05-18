using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Orders;

public class VolumeDiscount : EntityBase<int>
{
    public bool? IsApplyForAll {  get; set; }
    public int MinQuantity { get; set; }
    public double DiscountValue { get; set; }
    public bool IsPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public virtual ICollection<VolumeDiscountItem>? VolumeDiscountItems { get; set; }
    public virtual ICollection<ExcludeVolumeDiscountItem>? ExcludeVolumeDiscountItems { get; set; }
}
