using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Stores;

public class Store : EntityBase<Guid>, ISoftDelete
{
    public int ShopOwnerId { get; set; }
    public ShopOwner ShopOwner { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeletedName { get; set; }
}
