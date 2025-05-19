namespace ERP_Service.Domain.Const;

public static class EventWeights
{
    public static readonly EventInfo Purchase = new("Purchase", 5);
    public static readonly EventInfo AddToCart = new("AddToCart", 3);
    public static readonly EventInfo RemoveFromCart = new("RemoveFromCart", -3);
    public static readonly EventInfo ProductView = new("ProductView", 1);
    public static readonly EventInfo Search = new("Search", 1);
    public static readonly EventInfo AddToWishlist = new("AddToWishlist", 2);
}
public record EventInfo(string Name, int Weight);
