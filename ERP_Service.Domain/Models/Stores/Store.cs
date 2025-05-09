using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.Stores;

public class Store : EntityBase<Guid>, ISoftDelete
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public int Followers { get; set; }
    public DateTime JoinDate { get; set; }
    public bool Verified { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? Policies { get; set; } = null;
    public string Facebook { get; set; } = string.Empty;
    public string Instagram { get; set; } = string.Empty;
    public string Twitter { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeletedName { get; set; }
    public int? UserLoginId { get; set; }

    public UserLogin UserLogin {  get; set; }
}
