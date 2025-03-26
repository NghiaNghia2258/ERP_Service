using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Domain.Models.Stores;

public class Employee: EntityBase<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}
