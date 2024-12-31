namespace ERP_Service.Domain.Exceptions.Products;
public class OutOfStockException : Exception
{
public OutOfStockException() : base("Out of stock") { }
public OutOfStockException(string message) : base(message) { }
public OutOfStockException(string message, Exception inner) : base(message, inner) { }
}
