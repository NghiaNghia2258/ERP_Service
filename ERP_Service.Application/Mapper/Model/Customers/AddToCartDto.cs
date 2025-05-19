namespace ERP_Service.Application.Mapper.Model.Customers;

public class AddToCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImgUrl { get; set; }
}
