using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models;

public class UserEvent : EntityBase<Guid>
{
    public Guid UserId { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; } = DateTime.UtcNow;
    public int ProductId { get; set; }
    public int Weight { get; set; }
}
