using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.Models.Stores;

namespace ERP_Service.Domain.Models.Orders;

public class CartItem : EntityBase<int>
{
    public int CartId { get; set; }
    public Guid StoreId { get; set; }

    public int ProductVariantId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public bool? HasSelected { get; set; } = true;
    public virtual Cart Cart { get; set; } = null!;
    public virtual Store Store { get; set; } = null!;
    public virtual ProductVariant? ProductVariant { get; set; }  
}
