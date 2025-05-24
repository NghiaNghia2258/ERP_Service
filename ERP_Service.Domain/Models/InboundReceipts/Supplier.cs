using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.InboundReceipts;

public class Supplier: EntityBase<int>
{
    public string Name { get; set; }
    public Guid StoreId { get; set; }

    public Store Store { get; set; }
}
