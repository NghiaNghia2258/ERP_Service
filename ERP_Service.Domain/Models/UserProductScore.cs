namespace ERP_Service.Domain.Models;

public class UserProductScore
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ProductId { get; set; }
    public int Score { get; set; }
}
