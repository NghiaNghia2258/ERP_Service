using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;

namespace ERP_Service.Domain.Models.InboundReceipts;

public class InboundReceipt : EntityBase<Guid>, IAuditableEntity
{
    public DateTime StockInDate { get; set; } 
    public string? SupplierId { get; set; }  
    public string? Note { get; set; }
    public DateTime CreatedAt { get ; set ; }
    public string CreatedBy { get ; set ; }
    public string CreatedName { get ; set ; }
    public DateTime? UpdatedAt { get ; set ; }
    public string? UpdatedBy { get ; set ; }
    public string? UpdatedName { get ; set ; }
    public bool IsDeleted { get ; set ; }
    public DateTime? DeletedAt { get ; set ; }
    public string? DeletedBy { get ; set ; }
    public string? DeletedName { get ; set ; }

    public List<InboundReceiptItem> InboundReceiptItems { get; set; } = new();

}
