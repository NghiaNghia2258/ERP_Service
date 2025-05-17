namespace ERP_Service.Domain.Const;

public static class EventWeights
{
    public const int Purchase = 5;
    public const int AddToCart = 3;
    public const int RemoveFromCart = -3;
    public const int ProductView = 1;
    public const int Search = 1;
    public const int AddToWishlist = 2;
}
