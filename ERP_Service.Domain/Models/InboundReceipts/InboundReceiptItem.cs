using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Models.InboundReceipts;

public class InboundReceiptItem: EntityBase<int>
{
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public int Quantity { get; set; }   
    public decimal UnitPrice { get; set; }
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public InboundReceipt InboundReceipt { get; set; }
}
