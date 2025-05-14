
namespace ERP_Service.Application.Mapper.Model.Inbounds;

public class InboundUpdateDto
{
    public Guid Id { get; set; }
    public DateTime StockInDate { get; set; }
    public string? SupplierId { get; set; }
    public string? Note { get; set; }
    public List<InboundReceiptItemDto> Items { get; set; } = new();

}
public class InboundReceiptItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}