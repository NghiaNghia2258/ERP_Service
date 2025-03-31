using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Stores;

public class ShopOwner : EntityBase<Guid>, ISoftDelete
{
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeletedName { get; set; }
    public virtual ICollection<Store> Stores { get; set; }
}
