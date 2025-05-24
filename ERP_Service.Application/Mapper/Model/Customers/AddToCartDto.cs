namespace ERP_Service.Application.Mapper.Model.Customers;

public class AddToCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Property1 { get; set; }
    public string Property2 { get; set; }
}
