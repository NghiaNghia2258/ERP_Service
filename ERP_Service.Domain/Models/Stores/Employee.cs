using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Orders;

namespace ERP_Service.Domain.Models.Stores;

public class Employee: EntityBase<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    public virtual Order Order { get; set; }
}
