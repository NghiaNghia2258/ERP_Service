namespace ERP_Service.Application.Mapper.Model.Customers;

public class AddToCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
