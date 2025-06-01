namespace ERP_Service.Application.Mapper.Model.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Note { get; set; }
    public string CustomerName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
    public string? CustomerNote { get; set; }
    public int PaymentStatus { get; set; }
    public double DiscountPercent { get; set; }
    public double DiscountValue { get; set; }
    public double TotalPrice { get; set; }
    public string? VoucherCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public string CreatedName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public ShippingAddressDto ShippingAddress { get; set; } = default!;
    public List<OrderItemDto> OrderItems { get; set; } = new();
}

public class ShippingAddressDto
{
    public string FullAddress { get; set; } = default!;
    public string RecipientName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}

public class OrderItemDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public ProductVariantOrderDto ProductVariant { get; set; } = default!;
}

public class ProductVariantOrderDto
{
    public string Name { get; set; } = default!;
    public string PropertyName1 { get; set; } = default!;
    public string PropertyName2 { get; set; } = default!;
    public string PropertyValue1 { get; set; } = default!;
    public string PropertyValue2 { get; set; } = default!;
}