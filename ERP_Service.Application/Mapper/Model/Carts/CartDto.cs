namespace ERP_Service.Application.Mapper.Model.Carts;

public class CartDto
{
    public int Id { get; set; }
    public List<CartItemDto> CartItems { get; set; }
}
public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }  
    public string? ImageUrl { get; set; }  
    public int Quantity { get; set; }
    public bool? IsSelected { get; set; }
    public Guid? ShopId { get; set; }
    public string? ShopName { get; set; } = string.Empty;
    public string? ShopAvatarUrl { get; set; } 
}
